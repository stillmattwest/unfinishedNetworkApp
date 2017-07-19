﻿using System;
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
        public int ConnectedDeviceID{ get; set; }
        public int ReportingDeviceID { get; set; }
        public int? DepartmentID { get; set; }
        // Navigation Properties
        public virtual BDI BDI { get; set; }
        public virtual ConnectedDevice ConnectedDevice { get; set; }
        public virtual Department Department { get; set; }
        public virtual ReportingDevice ReportingDevice { get; set; }
    }
}