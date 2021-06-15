namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFeacturedEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeaturedEvents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        type = c.String(),
                        Description = c.String(),
                        EmployeeId = c.Long(),
                        UserId = c.Long(),
                        LastForemanId = c.Long(),
                        CurrentForemanId = c.Long(),
                        LastContractorId = c.Long(),
                        CurrentContractorId = c.Long(),
                        LastTypeEmployeeId = c.Long(),
                        CurrentTypeEmployeeId = c.Long(),
                        IngressDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contractors", t => t.CurrentForemanId)
                .ForeignKey("dbo.Employees", t => t.CurrentForemanId)
                .ForeignKey("dbo.TypeEmployees", t => t.CurrentTypeEmployeeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Contractors", t => t.LastContractorId)
                .ForeignKey("dbo.Employees", t => t.LastForemanId)
                .ForeignKey("dbo.TypeEmployees", t => t.LastTypeEmployeeId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.EmployeeId)
                .Index(t => t.UserId)
                .Index(t => t.LastForemanId)
                .Index(t => t.CurrentForemanId)
                .Index(t => t.LastContractorId)
                .Index(t => t.LastTypeEmployeeId)
                .Index(t => t.CurrentTypeEmployeeId);
            
            //DropColumn("dbo.IngressEgresses", "inForeman");
        }
        
        public override void Down()
        {
          //  AddColumn("dbo.IngressEgresses", "inForeman", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.FeaturedEvents", "UserId", "dbo.Users");
            DropForeignKey("dbo.FeaturedEvents", "LastTypeEmployeeId", "dbo.TypeEmployees");
            DropForeignKey("dbo.FeaturedEvents", "LastForemanId", "dbo.Employees");
            DropForeignKey("dbo.FeaturedEvents", "LastContractorId", "dbo.Contractors");
            DropForeignKey("dbo.FeaturedEvents", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.FeaturedEvents", "CurrentTypeEmployeeId", "dbo.TypeEmployees");
            DropForeignKey("dbo.FeaturedEvents", "CurrentForemanId", "dbo.Employees");
            DropForeignKey("dbo.FeaturedEvents", "CurrentForemanId", "dbo.Contractors");
            DropIndex("dbo.FeaturedEvents", new[] { "CurrentTypeEmployeeId" });
            DropIndex("dbo.FeaturedEvents", new[] { "LastTypeEmployeeId" });
            DropIndex("dbo.FeaturedEvents", new[] { "LastContractorId" });
            DropIndex("dbo.FeaturedEvents", new[] { "CurrentForemanId" });
            DropIndex("dbo.FeaturedEvents", new[] { "LastForemanId" });
            DropIndex("dbo.FeaturedEvents", new[] { "UserId" });
            DropIndex("dbo.FeaturedEvents", new[] { "EmployeeId" });
            DropTable("dbo.FeaturedEvents");
        }
    }
}
