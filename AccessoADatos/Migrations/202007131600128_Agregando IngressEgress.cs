namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoIngressEgress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngressEgresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RetiredBefore = c.Boolean(nullable: false),
                        DescriptionOut = c.String(),
                        IngressDateTime = c.DateTime(nullable: false),
                        EgressDateTime = c.DateTime(nullable: false),
                        EmployeeId = c.Long(),
                        LandId = c.Long(),
                        UserId = c.Long(),
                        IsSyncToVisma = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Lands", t => t.LandId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.EmployeeId)
                .Index(t => t.LandId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngressEgresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.IngressEgresses", "LandId", "dbo.Lands");
            DropForeignKey("dbo.IngressEgresses", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.IngressEgresses", new[] { "UserId" });
            DropIndex("dbo.IngressEgresses", new[] { "LandId" });
            DropIndex("dbo.IngressEgresses", new[] { "EmployeeId" });
            DropTable("dbo.IngressEgresses");
        }
    }
}
