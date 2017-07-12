using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using NetworkBillingSystem_Alpha.Models;


namespace NetworkBillingSystem_Alpha.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet <ConnectedDevice> ConnectedDevices {get; set;}
        public DbSet <BDI>BDIs { get; set; }
        public DbSet <Department> Departments { get; set; }
        public DbSet <Connection> Connections { get; set; }
        public DbSet <ReportingDevice> ReportingDevices { get; set; }

    }
}