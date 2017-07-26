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


        // TODO - Break this up into SRP functions
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

                // get BDI from database
                BDI bdi = db.BDIs
                    .Where(x => x.BDINumber == itemBdi)
                    .FirstOrDefault();

                // check to see if connectedDevice Mac exists in database
                // if not create it
                if (!db.ConnectedDevices.Any(x => x.Mac == itemMac))
                {
                    ConnectedDevice newConnDev = new ConnectedDevice();
                    newConnDev.Mac = itemMac;
                    db.ConnectedDevices.Add(newConnDev);
                }

                // Entity Framework stores this data to local datasource, not persisted to db yet.
                // grab ConnectedDevice from local datasource and assign it to cd
                var cd = db.ConnectedDevices.Local
                     .Where(x => x.Mac == itemMac)
                     .FirstOrDefault();

                // get reporting device id from router name
                var reportingDevice = db.ReportingDevices
                    .Where(x => x.DeviceName == itemRouter)
                    .FirstOrDefault();

                // create new Connection object
                Connection connection = new Connection();
                connection.ConnectionDateTime = DateTime.Now;
                connection.ReportingDeviceID = reportingDevice.ReportingDeviceID;

                // add connection to db
                db.Connections.Add(connection);

                // get new connection
                connection = db.Connections.Local
                    .OrderByDescending(x => x.ConnectionID)
                    .FirstOrDefault();

                // add connection to connected device
                cd.Connections.Add(connection);
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
            }

        }

    }
}