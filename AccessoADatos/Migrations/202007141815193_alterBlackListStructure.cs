namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterBlackListStructure : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserLands", newName: "LandUsers");
            DropForeignKey("dbo.BlackLists", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.BlackLists", new[] { "EmployeeId" });
            DropPrimaryKey("dbo.LandUsers");
            AddColumn("dbo.BlackLists", "Documento", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.LandUsers", new[] { "Land_Id", "User_Id" });
            DropColumn("dbo.BlackLists", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlackLists", "EmployeeId", c => c.Long(nullable: false));
            DropPrimaryKey("dbo.LandUsers");
            DropColumn("dbo.BlackLists", "Documento");
            AddPrimaryKey("dbo.LandUsers", new[] { "User_Id", "Land_Id" });
            CreateIndex("dbo.BlackLists", "EmployeeId");
            AddForeignKey("dbo.BlackLists", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.LandUsers", newName: "UserLands");
        }
    }
}
