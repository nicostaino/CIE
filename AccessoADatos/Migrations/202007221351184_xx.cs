namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xx : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "TypeEmployeeId", "dbo.TypeEmployees");
            DropIndex("dbo.Employees", new[] { "TypeEmployeeId" });
            AlterColumn("dbo.Employees", "TypeEmployeeId", c => c.Long());
            CreateIndex("dbo.Employees", "TypeEmployeeId");
            AddForeignKey("dbo.Employees", "TypeEmployeeId", "dbo.TypeEmployees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "TypeEmployeeId", "dbo.TypeEmployees");
            DropIndex("dbo.Employees", new[] { "TypeEmployeeId" });
            AlterColumn("dbo.Employees", "TypeEmployeeId", c => c.Long(nullable: false));
            CreateIndex("dbo.Employees", "TypeEmployeeId");
            AddForeignKey("dbo.Employees", "TypeEmployeeId", "dbo.TypeEmployees", "Id", cascadeDelete: true);
        }
    }
}
