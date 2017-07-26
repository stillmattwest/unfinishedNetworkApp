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
        public virtual ICollection<BDI> BDIs { get; set; }
        public Department()
        {
            this.BDIs = new List<BDI>();
        }
    }
}