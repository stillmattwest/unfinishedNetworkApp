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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BDINumber { get; set; }
        // Navigation properties
        public virtual Department Department { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
    }
}