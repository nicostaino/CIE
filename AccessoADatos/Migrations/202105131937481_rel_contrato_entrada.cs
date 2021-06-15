namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rel_contrato_entrada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IngressEgresses", "ContractId", c => c.Int(nullable: false));
            AddColumn("dbo.IngressEgresses", "Contract_Id", c => c.Long());
            CreateIndex("dbo.IngressEgresses", "Contract_Id");
            AddForeignKey("dbo.IngressEgresses", "Contract_Id", "dbo.Contracts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngressEgresses", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.IngressEgresses", new[] { "Contract_Id" });
            DropColumn("dbo.IngressEgresses", "Contract_Id");
            DropColumn("dbo.IngressEgresses", "ContractId");
        }
    }
}
