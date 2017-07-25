namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modDepartmentModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "ConnectedDevice_ConnectedDeviceID", "dbo.ConnectedDevices");
            DropIndex("dbo.Departments", new[] { "ConnectedDevice_ConnectedDeviceID" });
            CreateTable(
                "dbo.DepartmentConnectedDevices",
                c => new
                    {
                        Department_DepartmentID = c.Int(nullable: false),
                        ConnectedDevice_ConnectedDeviceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Department_DepartmentID, t.ConnectedDevice_ConnectedDeviceID })
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.ConnectedDevices", t => t.ConnectedDevice_ConnectedDeviceID, cascadeDelete: true)
                .Index(t => t.Department_DepartmentID)
                .Index(t => t.ConnectedDevice_ConnectedDeviceID);
            
            DropColumn("dbo.Departments", "ConnectedDevice_ConnectedDeviceID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "ConnectedDevice_ConnectedDeviceID", c => c.Int());
            DropForeignKey("dbo.DepartmentConnectedDevices", "ConnectedDevice_ConnectedDeviceID", "dbo.ConnectedDevices");
            DropForeignKey("dbo.DepartmentConnectedDevices", "Department_DepartmentID", "dbo.Departments");
            DropIndex("dbo.DepartmentConnectedDevices", new[] { "ConnectedDevice_ConnectedDeviceID" });
            DropIndex("dbo.DepartmentConnectedDevices", new[] { "Department_DepartmentID" });
            DropTable("dbo.DepartmentConnectedDevices");
            CreateIndex("dbo.Departments", "ConnectedDevice_ConnectedDeviceID");
            AddForeignKey("dbo.Departments", "ConnectedDevice_ConnectedDeviceID", "dbo.ConnectedDevices", "ConnectedDeviceID");
        }
    }
}
