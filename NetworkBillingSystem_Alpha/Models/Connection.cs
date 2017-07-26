using NetworkBillingSystem_Alpha.Models;
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
        public virtual int ReportingDeviceID { get; set; }
    }
}