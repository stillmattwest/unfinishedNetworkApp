using System;
using System.Collections.Generic;
using System.Linq;
using NetworkBillingSystem_Alpha.DAL;
using System.Text.RegularExpressions;
using NetworkBillingSystem_Alpha.Models;
using System.Data.Entity.Migrations;

namespace NetworkBillingSystem_Alpha.Utilities
{
    public class BillingDataMethods
    {
        private ApplicationContext db = new ApplicationContext();
        private SshMethods sshMethods = new SshMethods();

        public List<List<string>> GetBillingData()
        {
            // This method returns the connected Macs and the associated BDI interface data from a router, as well as the department associated with that BDI.

            // declare result list of string lists
            List<List<string>> result = new List<List<string>>();

            // get arp data from router
            var arpResult = sshMethods.RunSshCommand("show arp");
            var data = arpResult[0];
            var router = arpResult[1];
            //format data
            // splitting result by lines which guarantees we have matching macs and bdis
            string[] splitter = new string[] { Environment.NewLine };
            var dataArray = data.Split(splitter, StringSplitOptions.None);
            // convert dataArray to list for ease of use
            List<string> dataList = new List<string>(dataArray);

            // process the data from the router
            foreach (var line in dataList)
            {
                // checking if line both contains anything and has a BDI entry. If so, process, else drop it.
                if (!String.IsNullOrEmpty(line))
                {
                    if (Regex.IsMatch(line, @"BDI[0-9]{2,3}", RegexOptions.Multiline))
                    // match a mac address in the pattern of xxxx.xxxx.xxxx
                    {
                        // get the mac
                        Regex matchMac = new Regex(@"(([0-9A-Fa-f]){4}\.([0-9A-Fa-f]){4}\.([0-9A-Fa-f]){4}){1}");
                        string mac = matchMac.Match(line).ToString();
                        // add new string list to result list
                        result.Add(new List<string>());
                        // add mac to correct string array in result
                        result[result.Count - 1].Add(mac);
                        // get the BDI
                        Regex matchBdi = new Regex(@"BDI[0-9]{2,3}");
                        string bdi = matchBdi.Match(line).ToString();
                        // add BDI to correct string array in result
                        result[result.Count - 1].Add(bdi);
                        // call function to match department from bdi number
                        string department = getDepartmentFromInterface(bdi);
                        // add department to result list
                        result[result.Count - 1].Add(department);
                        // add reporting device to result list
                        result[result.Count - 1].Add(router);

                    }

                }
            } // end foreach loop

            addBillingDataToDB(result);

            return result;
        }


        // calls functions to assemble billing data for connectionInfo list and adds it all to database.
        public void addBillingDataToDB(List<List<string>> connectionInfo)
        {
            foreach (List<string> item in connectionInfo)
            {
                string itemMac = item[0];
                string itemBdi = item[1];
                string itemDepartment = item[2];
                string itemRouter = item[3];

                // autocreate new BDI if doesn't exist in database
                AutoCreateBDI(itemBdi);

                // Get BDI data
                BDI bdi = getBDIData(itemBdi);

                // autocreate new ConnectedDevice if doesn't exist in database
                autoCreateConnectedDevice(itemMac);

                // grab ConnectedDevice from local datasource and assign it to cd
                var cd = getConnectedDevice(itemMac);

                // get reporting device id from router name
                var reportingDevice = getReportingDevice(itemRouter);

                // create new Connection object
                createConnection(reportingDevice.ReportingDeviceID, itemMac);

                // get new connection
                Connection newestConnection = getNewestConnection();
                
                // add connection to connected device
                cd.Connections.Add(newestConnection);

                // add connected device to bdi
                bdi.ConnectedDevices.Add(cd);

                // Entity Framework is smart enough to track the changes above so we just save them
                db.SaveChanges();
            }

        }

        public string getDepartmentFromInterface(string bdi)
        {
            // use BDI to find department
            IQueryable<Department> dept = db.Departments;

            var deptName = dept
                .Where(x => x.BDIs.Any(d => d.BDINumber == bdi))
                .Select(x => x.Name)
                .FirstOrDefault();

            if (!String.IsNullOrEmpty(deptName))
            {
                return deptName;
            }

            return "unknown";

        }

        // auto-create new BDI if it doesn't exist in database 
        //TODO - notify administrator that a new BDI has been reported and needs additional data.
        public void AutoCreateBDI(string bdi)
        {
            if (!db.BDIs.Any(x => x.BDINumber == bdi))
            {
                BDI newBdi = new BDI();
                newBdi.BDINumber = bdi;
                db.BDIs.Add(newBdi);
                db.SaveChanges();
            }

        }

        // returns BDI from either DB or local data storage
        public BDI getBDIData(string bdiNumber)
        {
            try
            {
                BDI bdi = db.BDIs
                    .Where(x => x.BDINumber == bdiNumber)
                    .FirstOrDefault();
                return bdi;
            }
            catch (NullReferenceException)
            {
                BDI bdi = db.BDIs.Local
                    .Where(x => x.BDINumber == bdiNumber)
                    .FirstOrDefault();
                return bdi;
            }


        }

        // checks to see if a connected device exists in database and adds it if not
        public void autoCreateConnectedDevice(string macAddress)
        {
            if (!db.ConnectedDevices.Any(x => x.Mac == macAddress))
            {
                ConnectedDevice newConnDev = new ConnectedDevice();
                newConnDev.Mac = macAddress;
                db.ConnectedDevices.Add(newConnDev);
                db.SaveChanges();
            }
        }

        // returns a connected device either from database or local data storage
        public ConnectedDevice getConnectedDevice(string macAddress)
        {
            try
            {
                var cd = db.ConnectedDevices
                    .Where(x => x.Mac == macAddress)
                    .FirstOrDefault();

                return cd;
            }
            catch (NullReferenceException)
            {
                var cd = db.ConnectedDevices.Local
                    .Where(x => x.Mac == macAddress)
                    .FirstOrDefault();

                return cd;
            }
            
        }

        // returns a reporting device from database
        public ReportingDevice getReportingDevice(string routerName)
        {
            var reportingDevice = db.ReportingDevices
                    .Where(x => x.DeviceName == routerName)
                    .FirstOrDefault();

            return reportingDevice;
        }

        // adds a new connection object to database
        public void createConnection(int routerId, string macAddress)
        {
            Connection connection = new Connection();
            connection.ConnectionDateTime = DateTime.Now;
            connection.ReportingDeviceID = routerId;
            connection.Mac = macAddress;
            db.Connections.Add(connection);
            db.SaveChanges();
        }

        // returns the most recently added connection from database or from local data storage, whichever is newer.
        public Connection getNewestConnection()
        {
            Connection dbConnection = db.Connections
                    .OrderByDescending(x => x.ConnectionID)
                    .FirstOrDefault();

            Connection localConnection = db.Connections.Local
                .OrderByDescending(x => x.ConnectionID)
                    .FirstOrDefault();

            if(localConnection.ConnectionID >= dbConnection.ConnectionID)
            {
                return localConnection;
            }

            return dbConnection;
        }

    }
}