namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addconstractors : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.IngressEgresses", name: "constratista_Id", newName: "ContractorId");
            RenameIndex(table: "dbo.IngressEgresses", name: "IX_constratista_Id", newName: "IX_ContractorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.IngressEgresses", name: "IX_ContractorId", newName: "IX_constratista_Id");
            RenameColumn(table: "dbo.IngressEgresses", name: "ContractorId", newName: "constratista_Id");
        }
    }
}
