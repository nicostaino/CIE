namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4Migracion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Temperatures", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Temperatures", new[] { "EmployeeId" });
            AlterColumn("dbo.Temperatures", "EmployeeId", c => c.Long());
            CreateIndex("dbo.Temperatures", "EmployeeId");
            AddForeignKey("dbo.Temperatures", "EmployeeId", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Temperatures", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Temperatures", new[] { "EmployeeId" });
            AlterColumn("dbo.Temperatures", "EmployeeId", c => c.Long(nullable: false));
            CreateIndex("dbo.Temperatures", "EmployeeId");
            AddForeignKey("dbo.Temperatures", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
