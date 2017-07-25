namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07242017_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConnectedDevices", "DepartmentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConnectedDevices", "DepartmentID");
        }
    }
}
