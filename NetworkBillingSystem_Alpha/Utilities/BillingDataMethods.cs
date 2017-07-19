using System;
using System.Collections.Generic;
using System.Linq;
using NetworkBillingSystem_Alpha.DAL;
using System.Text.RegularExpressions;
using NetworkBillingSystem_Alpha.Models;

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
                        string department = getDepartmentForInterface(bdi);
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

        // getDepartmentForInterface queries the database for the department associated with a BDI and returns either that or 'unknown.'
        public string getDepartmentForInterface(string bdi)
        {
            IQueryable<BDI> bdiData = db.BDIs;
            var department = bdiData
                            .Where(d => d.BDINumber == bdi)
                            .Select(d => d.Department.Name)
                            .FirstOrDefault();
            if (department != null)
            {
                return department;
            }
            else
            {
                return "unknown";
            }
        }

        public void addBillingDataToDB(List<List<string>> connectionInfo)
        {
            foreach (List<string> item in connectionInfo)
            {

                string itemMac = item[0];
                string itemBdi = item[1];
                string itemDepartment = item[2];
                string itemRouter = item[3];

                ConnectedDevice cd = new ConnectedDevice();
                Connection con = new Connection();

                cd.Mac = itemMac;

                // check database for existing connected device. If not there, add it.
                if (!db.ConnectedDevices.Any(x => x.Mac == cd.Mac))
                {
                    db.ConnectedDevices.Add(cd);
                  //  db.SaveChanges();
                }

                // get ConnectedDeviceID from database
                IQueryable<ConnectedDevice> connectedDevices = db.ConnectedDevices;
                int cdId = connectedDevices
                    .Where(x => x.Mac == itemMac)
                    .Select(x => x.ConnectedDeviceID)
                    .FirstOrDefault();

                // get reporting device id from router name
                IQueryable<ReportingDevice> rdData = db.ReportingDevices;
                int reportingDeviceID = rdData
                    .Where(x => x.DeviceName == itemRouter)
                    .Select(x => x.ReportingDeviceID)
                    .FirstOrDefault();

                // auto-create new BDI if it doesn't exist in database - TODO - notify administrator that a new BDI has been reported and needs additional data.
                if(!db.BDIs.Any(x => x.BDINumber == itemBdi))
                {
                    BDI bdi = new BDI();
                    bdi.BDINumber = itemBdi;
                    db.BDIs.Add(bdi);
                }

                // get departmentID from BDI database. departmentID is a nullable field
                IQueryable<BDI> bdiLIst = db.BDIs;
                var departmentID = bdiLIst
                    .Where(x => x.BDINumber == itemBdi)
                    .Select(x => x.DepartmentID)
                    .FirstOrDefault();

                con.ConnectionDateTime = DateTime.Now;
                con.BDINumber = itemBdi;
                con.ConnectedDeviceID = cdId;
                con.ReportingDeviceID = reportingDeviceID;
                con.DepartmentID = departmentID;
                
                

                db.Connections.Add(con);
                db.SaveChanges();
            }

        }

    }
}