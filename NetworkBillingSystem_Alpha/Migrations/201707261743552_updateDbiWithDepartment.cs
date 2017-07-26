namespace NetworkBillingSystem_Alpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDbiWithDepartment : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BDIs", name: "Department_DepartmentID", newName: "DepartmentID");
            RenameIndex(table: "dbo.BDIs", name: "IX_Department_DepartmentID", newName: "IX_DepartmentID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BDIs", name: "IX_DepartmentID", newName: "IX_Department_DepartmentID");
            RenameColumn(table: "dbo.BDIs", name: "DepartmentID", newName: "Department_DepartmentID");
        }
    }
}
