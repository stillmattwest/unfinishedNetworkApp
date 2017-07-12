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
        [ForeignKey("BDINumber")]
        public virtual ICollection<BDI> BDIs { get; set; }
        [ForeignKey("ConnectionID")]
        public virtual ICollection<Connection> Connections { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual ICollection<Department> Departments { get; set; }
    }
}