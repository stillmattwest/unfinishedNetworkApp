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

            // declare queryable variable for BDIs. This is where we get the departments from
            IQueryable<BDI> bdiData = db.BDIs;

            // get arp data from router
            var data = sshMethods.RunSshCommand("show arp");
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
                        // query the database to match the BDI number
                        var department = bdiData
                            .Where(d => d.BDINumber == bdi)
                            .Select(d => d.Department.Name)
                            .FirstOrDefault();
                        // add department to result list
                        
                        result[result.Count - 1].Add(department);
                    }

                }
            } // end foreach loop

            return result;
        }


        public void AddBillingData()
        {

        }
    }
}