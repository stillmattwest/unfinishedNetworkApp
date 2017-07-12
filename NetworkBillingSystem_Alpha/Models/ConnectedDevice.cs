using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetworkBillingSystem_Alpha.Models
{
    public class ConnectedDevice
    {
        [Key]
        public string Mac { get; set; }
        public virtual ICollection<BDI> BDIs { get; set; }
        public virtual ICollection<Connection> Connection { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}