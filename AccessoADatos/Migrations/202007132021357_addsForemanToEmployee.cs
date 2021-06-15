namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsForemanToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmployeeId", c => c.Long());
            CreateIndex("dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.Employees", "EmployeeId", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {

            DropForeignKey("dbo.Employees", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "EmployeeId" });
            DropColumn("dbo.Employees", "EmployeeId");
        }
    }
}
