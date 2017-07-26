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
        public int ConnectedDeviceID { get; set; }
        public string Mac { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
       
        public ConnectedDevice()
        {
            Connections = new List<Connection>();
        }
    }
}