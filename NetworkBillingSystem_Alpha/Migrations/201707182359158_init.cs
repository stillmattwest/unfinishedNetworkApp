namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BDIs",
                c => new
                    {
                        BDINumber = c.String(nullable: false, maxLength: 128),
                        DepartmentID = c.Int(),
                        ConnectedDevice_ConnectedDeviceID = c.Int(),
                    })
                .PrimaryKey(t => t.BDINumber)
                .ForeignKey("dbo.ConnectedDevices", t => t.ConnectedDevice_ConnectedDeviceID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .Index(t => t.DepartmentID)
                .Index(t => t.ConnectedDevice_ConnectedDeviceID);
            
            CreateTable(
                "dbo.Connections",
                c => new
                    {
                        ConnectionID = c.Int(nullable: false, identity: true),
                        ConnectionDateTime = c.DateTime(nullable: false),
                        BDINumber = c.String(maxLength: 128),
                        ConnectedDeviceID = c.Int(nullable: false),
                        ReportingDeviceID = c.Int(nullable: false),
                        DepartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.ConnectionID)
                .ForeignKey("dbo.BDIs", t => t.BDINumber)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.ConnectedDevices", t => t.ConnectedDeviceID, cascadeDelete: true)
                .ForeignKey("dbo.ReportingDevices", t => t.ReportingDeviceID, cascadeDelete: true)
                .Index(t => t.BDINumber)
                .Index(t => t.ConnectedDeviceID)
                .Index(t => t.ReportingDeviceID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.ConnectedDevices",
                c => new
                    {
                        ConnectedDeviceID = c.Int(nullable: false, identity: true),
                        Mac = c.String(),
                    })
                .PrimaryKey(t => t.ConnectedDeviceID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BillingCode = c.String(),
                        ConnectedDevice_ConnectedDeviceID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.ConnectedDevices", t => t.ConnectedDevice_ConnectedDeviceID)
                .Index(t => t.ConnectedDevice_ConnectedDeviceID);
            
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
            DropForeignKey("dbo.Connections", "ReportingDeviceID", "dbo.ReportingDevices");
            DropForeignKey("dbo.Connections", "ConnectedDeviceID", "dbo.ConnectedDevices");
            DropForeignKey("dbo.Departments", "ConnectedDevice_ConnectedDeviceID", "dbo.ConnectedDevices");
            DropForeignKey("dbo.Connections", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.BDIs", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.BDIs", "ConnectedDevice_ConnectedDeviceID", "dbo.ConnectedDevices");
            DropForeignKey("dbo.Connections", "BDINumber", "dbo.BDIs");
            DropIndex("dbo.Departments", new[] { "ConnectedDevice_ConnectedDeviceID" });
            DropIndex("dbo.Connections", new[] { "DepartmentID" });
            DropIndex("dbo.Connections", new[] { "ReportingDeviceID" });
            DropIndex("dbo.Connections", new[] { "ConnectedDeviceID" });
            DropIndex("dbo.Connections", new[] { "BDINumber" });
            DropIndex("dbo.BDIs", new[] { "ConnectedDevice_ConnectedDeviceID" });
            DropIndex("dbo.BDIs", new[] { "DepartmentID" });
            DropTable("dbo.ReportingDevices");
            DropTable("dbo.Departments");
            DropTable("dbo.ConnectedDevices");
            DropTable("dbo.Connections");
            DropTable("dbo.BDIs");
        }
    }
}
