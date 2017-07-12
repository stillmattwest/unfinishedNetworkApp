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

        [ForeignKey("BDINumber")]
        public string BDI { get; set; }
        [ForeignKey("Mac")]
        public string Mac { get; set; }
        [ForeignKey("DepartmentID")]
        public int Department { get; set; }
        [ForeignKey("ReportingDeviceID")]
        public int ReportingDevice { get; set; }
    }
}