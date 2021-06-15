namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoBlackListandconstractors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlackLists",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        AdmissionDate = c.DateTime(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlackLists", "UserId", "dbo.Users");
            DropForeignKey("dbo.BlackLists", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.BlackLists", new[] { "EmployeeId" });
            DropIndex("dbo.BlackLists", new[] { "UserId" });
            DropTable("dbo.Contractors");
            DropTable("dbo.BlackLists");
        }
    }
}
