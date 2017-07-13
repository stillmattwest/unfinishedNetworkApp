using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetworkBillingSystem_Alpha.Models
{
    public class ReportingDevice
    {
        [Key]
        public int ReportingDeviceID { get; set; }
        public string DeviceName{ get; set; }
        public string IPAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}