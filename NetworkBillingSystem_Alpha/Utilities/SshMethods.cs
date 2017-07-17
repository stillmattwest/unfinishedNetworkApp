using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Renci.SshNet;
using NetworkBillingSystem_Alpha.DAL;

namespace NetworkBillingSystem_Alpha.Utilities
{
    public class SshMethods
    {
        private ApplicationContext db = new ApplicationContext();

        public List<string> RunSshCommand(string command)
        {
            var reportingDevices = db.ReportingDevices.ToList();
            var router = reportingDevices[0];
            var routerName = router.DeviceName;
            var Ip = router.IPAddress;
            var user = router.UserName;
            var pass = router.Password;

            SshClient ssh = new SshClient(Ip, user, pass);
            ssh.Connect();
            var data = ssh.RunCommand(command);
            ssh.Disconnect();

            return new List<string> { data.Result, routerName };
        }
    }
}