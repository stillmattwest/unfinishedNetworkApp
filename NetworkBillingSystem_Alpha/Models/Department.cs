using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetworkBillingSystem_Alpha.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string BillingCode { get; set; }
        // Navigation Properties
        public virtual ICollection<BDI>BDIs { get; set; }
        public virtual ICollection<Connection>Connections{ get; set; }
        public virtual ICollection<ConnectedDevice> ConnectedDevices { get; set;}


    }
}