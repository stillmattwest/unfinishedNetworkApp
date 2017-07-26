namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModelCollections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BDIs",
                c => new
                    {
                        BDIID = c.Int(nullable: false, identity: true),
                        BDINumber = c.String(),
                        Department_DepartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.BDIID)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentID)
                .Index(t => t.Department_DepartmentID);
            
            CreateTable(
                "dbo.ConnectedDevices",
                c => new
                    {
                        ConnectedDeviceID = c.Int(nullable: false, identity: true),
                        Mac = c.String(),
                        BDI_BDIID = c.Int(),
                    })
                .PrimaryKey(t => t.ConnectedDeviceID)
                .ForeignKey("dbo.BDIs", t => t.BDI_BDIID)
                .Index(t => t.BDI_BDIID);
            
            CreateTable(
                "dbo.Connections",
                c => new
                    {
                        ConnectionID = c.Int(nullable: false, identity: true),
                        ConnectionDateTime = c.DateTime(nullable: false),
                        ReportingDeviceID = c.Int(nullable: false),
                        ConnectedDevice_ConnectedDeviceID = c.Int(),
                    })
                .PrimaryKey(t => t.ConnectionID)
                .ForeignKey("dbo.ConnectedDevices", t => t.ConnectedDevice_ConnectedDeviceID)
                .Index(t => t.ConnectedDevice_ConnectedDeviceID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BillingCode = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.ReportingDevices",
                c => new
                    {
                        ReportingDeviceID = c.Int(nullable: false, identity: true),
                        DeviceName = c.String(),
                        IPAddress = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ReportingDeviceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BDIs", "Department_DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.ConnectedDevices", "BDI_BDIID", "dbo.BDIs");
            DropForeignKey("dbo.Connections", "ConnectedDevice_ConnectedDeviceID", "dbo.ConnectedDevices");
            DropIndex("dbo.Connections", new[] { "ConnectedDevice_ConnectedDeviceID" });
            DropIndex("dbo.ConnectedDevices", new[] { "BDI_BDIID" });
            DropIndex("dbo.BDIs", new[] { "Department_DepartmentID" });
            DropTable("dbo.ReportingDevices");
            DropTable("dbo.Departments");
            DropTable("dbo.Connections");
            DropTable("dbo.ConnectedDevices");
            DropTable("dbo.BDIs");
        }
    }
}
