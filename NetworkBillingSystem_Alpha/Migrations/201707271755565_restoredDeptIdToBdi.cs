namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restoredDeptIdToBdi : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BDIs", name: "Department_DepartmentID", newName: "DepartmentId");
            RenameIndex(table: "dbo.BDIs", name: "IX_Department_DepartmentID", newName: "IX_DepartmentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BDIs", name: "IX_DepartmentId", newName: "IX_Department_DepartmentID");
            RenameColumn(table: "dbo.BDIs", name: "DepartmentId", newName: "Department_DepartmentID");
        }
    }
}
