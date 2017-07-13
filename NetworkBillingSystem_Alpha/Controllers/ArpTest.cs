using System.Web.Mvc;
using NetworkBillingSystem_Alpha.DAL;
using NetworkBillingSystem_Alpha.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetworkBillingSystem_Alpha.Controllers
{
    public class ArpTestController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        private SshMethods sshMethods = new SshMethods();

        [HttpGet]
        public JsonResult getArp()
        {
            var data = sshMethods.RunSshCommand("show arp").Split(',');

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult getBillingData()
        {
            // This method returns the connected Macs and the associated BDI interface data from a router.

            // declare result list of string lists
            List<List<string>> result = new List<List<string>>();

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
                // checking if line contains anything and has a BDI entry. If so, process, else drop it.
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
                        result[result.Count -1].Add(mac);
                        // get the BDI
                        Regex matchBdi = new Regex(@"BDI[0-9]{2,3}");
                        string bdi = matchBdi.Match(line).ToString();
                        // add BDI to correct string array in result
                        result[result.Count -1].Add(bdi);
                    }

                }
            } // end foreach loop

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}