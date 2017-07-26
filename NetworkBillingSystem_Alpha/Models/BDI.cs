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
        public int BDIID { get; set; }
        public string BDINumber { get; set; }
        public int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<ConnectedDevice> ConnectedDevices { get; set; }

        public BDI()
        {
            ConnectedDevices = new List<ConnectedDevice>();
            Department = new Department();
        }

    }
}