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
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Index(IsUnique = true)]
        public string BillingCode { get; set; }
        [ForeignKey("BDINumber")]
        public ICollection<BDI>BDIs { get; set; }
        [ForeignKey("ConnectionID")]
        public ICollection<Connection>Connections{ get; set; }


    }
}