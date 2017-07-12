using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetworkBillingSystem_Alpha.Models
{
    public class BDI
    {
        [Key]
        public string BDINumber { get; set; }
        [ForeignKey("DepartmentID")]
        public string Department { get; set; }
        [ForeignKey("ConnectionID")]
        public ICollection<Connection> Connections { get; set; }
    }
}