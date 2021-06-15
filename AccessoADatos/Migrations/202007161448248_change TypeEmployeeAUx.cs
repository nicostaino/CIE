namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTypeEmployeeAUx : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "TypeEmployee_Id", "dbo.TypeEmployees");
            DropIndex("dbo.Employees", new[] { "TypeEmployee_Id" });
            RenameColumn(table: "dbo.Employees", name: "TypeEmployee_Id", newName: "TypeEmployeeId");
            AlterColumn("dbo.Employees", "TypeEmployeeId", c => c.Long(nullable: true));
            CreateIndex("dbo.Employees", "TypeEmployeeId");
            AddForeignKey("dbo.Employees", "TypeEmployeeId", "dbo.TypeEmployees", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "TypeEmployeeId", "dbo.TypeEmployees");
            DropIndex("dbo.Employees", new[] { "TypeEmployeeId" });
            AlterColumn("dbo.Employees", "TypeEmployeeId", c => c.Long());
            RenameColumn(table: "dbo.Employees", name: "TypeEmployeeId", newName: "TypeEmployee_Id");
            CreateIndex("dbo.Employees", "TypeEmployee_Id");
            AddForeignKey("dbo.Employees", "TypeEmployee_Id", "dbo.TypeEmployees", "Id");
        }
    }
}
