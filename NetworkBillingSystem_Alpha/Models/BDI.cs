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
        public virtual ICollection<ConnectedDevice> ConnectedDevices {get; set;}
  
        public BDI()
        {
            this.ConnectedDevices = new List<ConnectedDevice>();
        }

    }
}