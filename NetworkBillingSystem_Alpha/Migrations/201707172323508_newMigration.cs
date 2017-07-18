namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Connections", "ReportingDevice_ReportingDeviceID", "dbo.ReportingDevices");
            DropIndex("dbo.Connections", new[] { "ReportingDevice_ReportingDeviceID" });
            RenameColumn(table: "dbo.Connections", name: "BDI_BDINumber", newName: "BDINumber");
            RenameColumn(table: "dbo.Connections", name: "ConnectedDevice_Mac", newName: "Mac");
            RenameColumn(table: "dbo.Connections", name: "ReportingDevice_ReportingDeviceID", newName: "ReportingDeviceID");
            RenameIndex(table: "dbo.Connections", name: "IX_BDI_BDINumber", newName: "IX_BDINumber");
            RenameIndex(table: "dbo.Connections", name: "IX_ConnectedDevice_Mac", newName: "IX_Mac");
            AlterColumn("dbo.Connections", "ReportingDeviceID", c => c.Int(nullable: false));
            CreateIndex("dbo.Connections", "ReportingDeviceID");
            AddForeignKey("dbo.Connections", "ReportingDeviceID", "dbo.ReportingDevices", "ReportingDeviceID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Connections", "ReportingDeviceID", "dbo.ReportingDevices");
            DropIndex("dbo.Connections", new[] { "ReportingDeviceID" });
            AlterColumn("dbo.Connections", "ReportingDeviceID", c => c.Int());
            RenameIndex(table: "dbo.Connections", name: "IX_Mac", newName: "IX_ConnectedDevice_Mac");
            RenameIndex(table: "dbo.Connections", name: "IX_BDINumber", newName: "IX_BDI_BDINumber");
            RenameColumn(table: "dbo.Connections", name: "ReportingDeviceID", newName: "ReportingDevice_ReportingDeviceID");
            RenameColumn(table: "dbo.Connections", name: "Mac", newName: "ConnectedDevice_Mac");
            RenameColumn(table: "dbo.Connections", name: "BDINumber", newName: "BDI_BDINumber");
            CreateIndex("dbo.Connections", "ReportingDevice_ReportingDeviceID");
            AddForeignKey("dbo.Connections", "ReportingDevice_ReportingDeviceID", "dbo.ReportingDevices", "ReportingDeviceID");
        }
    }
}
