namespace NetworkBillingSystem_Alpha.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NetworkBillingSystem_Alpha.DAL.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NetworkBillingSystem_Alpha.DAL.ApplicationContext context)
        {
            context.Departments.AddOrUpdate(
                d => d.Name,
                new Department { Name = "Information Technology", BillingCode = "todo", },
                new Department { Name = "Grand Jury", BillingCode = "todo", },
                new Department { Name = "Public Health", BillingCode = "todo", },
                new Department { Name = "Environmental Health", BillingCode = "todo", },
                new Department { Name = "Public Works", BillingCode = "todo", },
                new Department { Name = "Veterans Administration", BillingCode = "todo", },
                new Department { Name = "Pension Trust", BillingCode = "todo", },
                new Department { Name = "Airport", BillingCode = "todo", },
                new Department { Name = "Cal Fire", BillingCode = "todo", },
                new Department { Name = "Staff Wireless", BillingCode = "todo", },
                new Department { Name = "Public Wireless", BillingCode = "todo", },
                new Department { Name = "Mental Health", BillingCode = "todo", },
                new Department { Name = "Sheriff Dept", BillingCode = "todo", },
                new Department { Name = "Health Agency", BillingCode = "todo", },
                new Department { Name = "Planning Dept", BillingCode = "todo", },
                new Department { Name = "Assessor", BillingCode = "todo", },
                new Department { Name = "Clerk Recorder", BillingCode = "todo", },
                new Department { Name = "Probation", BillingCode = "todo", },
                new Department { Name = "OES", BillingCode = "todo", },
                new Department { Name = "ITD Comm", BillingCode = "todo", },
                new Department { Name = "Animal Services", BillingCode = "todo", },
                new Department { Name = "DSS", BillingCode = "todo", },
                new Department { Name = "County Counsel", BillingCode = "todo", },
                new Department { Name = "District Attorney", BillingCode = "todo", },
                new Department { Name = "Auditor", BillingCode = "todo", },
                new Department { Name = "Farm Advisory", BillingCode = "todo", },
                new Department { Name = "Ag Comm", BillingCode = "todo", },
                new Department { Name = "Child Support Services", BillingCode = "todo", },
                new Department { Name = "Public Defender", BillingCode = "todo", },
                new Department { Name = "Courts", BillingCode = "todo", },
                new Department { Name = "", BillingCode = "todo", }


                );

            context.ReportingDevices.AddOrUpdate(
                d => d.DeviceName,
                new ReportingDevice { DeviceName = "mdcR", IPAddress = "10.192.1.1", UserName = "Manager", Password = "G00dbye April." }

                );
        }
    }
}
