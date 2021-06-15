namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contracts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TypeEmployees", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.TypeEmployees", new[] { "Contract_Id" });
            CreateTable(
                "dbo.ContractTypeEmployees",
                c => new
                    {
                        Contract_Id = c.Long(nullable: false),
                        TypeEmployee_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contract_Id, t.TypeEmployee_Id })
                .ForeignKey("dbo.Contracts", t => t.Contract_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeEmployees", t => t.TypeEmployee_Id, cascadeDelete: true)
                .Index(t => t.Contract_Id)
                .Index(t => t.TypeEmployee_Id);
            
            DropColumn("dbo.TypeEmployees", "Contract_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TypeEmployees", "Contract_Id", c => c.Long());
            DropForeignKey("dbo.ContractTypeEmployees", "TypeEmployee_Id", "dbo.TypeEmployees");
            DropForeignKey("dbo.ContractTypeEmployees", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.ContractTypeEmployees", new[] { "TypeEmployee_Id" });
            DropIndex("dbo.ContractTypeEmployees", new[] { "Contract_Id" });
            DropTable("dbo.ContractTypeEmployees");
            CreateIndex("dbo.TypeEmployees", "Contract_Id");
            AddForeignKey("dbo.TypeEmployees", "Contract_Id", "dbo.Contracts", "Id");
        }
    }
}
