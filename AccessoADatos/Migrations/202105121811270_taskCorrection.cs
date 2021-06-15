namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskCorrection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContractTypeEmployees", "Contract_Id", "dbo.Contracts");
            DropForeignKey("dbo.ContractTypeEmployees", "TypeEmployee_Id", "dbo.TypeEmployees");
            DropIndex("dbo.ContractTypeEmployees", new[] { "Contract_Id" });
            DropIndex("dbo.ContractTypeEmployees", new[] { "TypeEmployee_Id" });
            AddColumn("dbo.TypeEmployees", "Contract_Id", c => c.Long());
            CreateIndex("dbo.TypeEmployees", "Contract_Id");
            AddForeignKey("dbo.TypeEmployees", "Contract_Id", "dbo.Contracts", "Id");
            DropTable("dbo.ContractTypeEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContractTypeEmployees",
                c => new
                    {
                        Contract_Id = c.Long(nullable: false),
                        TypeEmployee_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contract_Id, t.TypeEmployee_Id });
            
            DropForeignKey("dbo.TypeEmployees", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.TypeEmployees", new[] { "Contract_Id" });
            DropColumn("dbo.TypeEmployees", "Contract_Id");
            CreateIndex("dbo.ContractTypeEmployees", "TypeEmployee_Id");
            CreateIndex("dbo.ContractTypeEmployees", "Contract_Id");
            AddForeignKey("dbo.ContractTypeEmployees", "TypeEmployee_Id", "dbo.TypeEmployees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContractTypeEmployees", "Contract_Id", "dbo.Contracts", "Id", cascadeDelete: true);
        }
    }
}
