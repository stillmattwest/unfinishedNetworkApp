namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clearedDataBase20170726 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Connections", "Mac", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Connections", "Mac");
        }
    }
}
