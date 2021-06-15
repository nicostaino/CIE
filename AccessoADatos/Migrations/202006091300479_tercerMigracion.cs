namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tercerMigracion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Temperatures", "User_Id", "dbo.Users");
            DropIndex("dbo.Temperatures", new[] { "User_Id" });
            RenameColumn(table: "dbo.Temperatures", name: "User_Id", newName: "UserId");
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nombre = c.String(),
                        Documento = c.String(),
                        LandId = c.Long(nullable: false),
                        IsDeletedByAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lands", t => t.LandId, cascadeDelete: true)
                .Index(t => t.LandId);
            
            AddColumn("dbo.Temperatures", "EmployeeId", c => c.Long(nullable: false));
            AlterColumn("dbo.Temperatures", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.Temperatures", "UserId");
            CreateIndex("dbo.Temperatures", "EmployeeId");
            AddForeignKey("dbo.Temperatures", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Temperatures", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Temperatures", "UserId", "dbo.Users");
            DropForeignKey("dbo.Temperatures", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "LandId", "dbo.Lands");
            DropIndex("dbo.Temperatures", new[] { "EmployeeId" });
            DropIndex("dbo.Temperatures", new[] { "UserId" });
            DropIndex("dbo.Employees", new[] { "LandId" });
            AlterColumn("dbo.Temperatures", "UserId", c => c.Long());
            DropColumn("dbo.Temperatures", "EmployeeId");
            DropTable("dbo.Employees");
            RenameColumn(table: "dbo.Temperatures", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Temperatures", "User_Id");
            AddForeignKey("dbo.Temperatures", "User_Id", "dbo.Users", "Id");
        }
    }
}
