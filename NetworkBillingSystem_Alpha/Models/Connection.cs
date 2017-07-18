using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetworkBillingSystem_Alpha.Models
{
    public class Connection
    {
        [Key]
        public int ConnectionID { get; set; }
        public DateTime ConnectionDateTime { get; set; }
        [ForeignKey("BDI")]
        public string BDINumber { get; set; }
        [ForeignKey("ConnectedDevice")]
        public string Mac { get; set; }
        public int ReportingDeviceID { get; set; }
        // Navigation Properties
        public virtual BDI BDI { get; set; }
        public virtual ConnectedDevice ConnectedDevice { get; set; }
        public virtual Department Department { get; set; }
        public virtual ReportingDevice ReportingDevice { get; set; }
    }
}