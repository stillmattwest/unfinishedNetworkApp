namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedConnectionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Connections", "DeviceName", c => c.String());
            CreateIndex("dbo.Connections", "ReportingDeviceID");
            AddForeignKey("dbo.Connections", "ReportingDeviceID", "dbo.ReportingDevices", "ReportingDeviceID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Connections", "ReportingDeviceID", "dbo.ReportingDevices");
            DropIndex("dbo.Connections", new[] { "ReportingDeviceID" });
            DropColumn("dbo.Connections", "DeviceName");
        }
    }
}
