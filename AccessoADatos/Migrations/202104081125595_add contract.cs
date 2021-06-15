namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcontract : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ContractorId = c.Long(),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        LandId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contractors", t => t.ContractorId)
                .ForeignKey("dbo.Lands", t => t.LandId)
                .Index(t => t.ContractorId)
                .Index(t => t.LandId);
            
            AddColumn("dbo.TypeEmployees", "Contract_Id", c => c.Long());
            CreateIndex("dbo.TypeEmployees", "Contract_Id");
            AddForeignKey("dbo.TypeEmployees", "Contract_Id", "dbo.Contracts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeEmployees", "Contract_Id", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "LandId", "dbo.Lands");
            DropForeignKey("dbo.Contracts", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Contracts", new[] { "LandId" });
            DropIndex("dbo.Contracts", new[] { "ContractorId" });
            DropIndex("dbo.TypeEmployees", new[] { "Contract_Id" });
            DropColumn("dbo.TypeEmployees", "Contract_Id");
            DropTable("dbo.Contracts");
        }
    }
}
