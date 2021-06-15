namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class viewupdate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ContractTypeEmployees", newName: "TypeEmployeeContracts");
            DropPrimaryKey("dbo.TypeEmployeeContracts");
            AddColumn("dbo.Employees", "ContractId", c => c.Int());
            AddColumn("dbo.Employees", "Contract_Id", c => c.Long());
            AddPrimaryKey("dbo.TypeEmployeeContracts", new[] { "TypeEmployee_Id", "Contract_Id" });
            CreateIndex("dbo.Employees", "Contract_Id");
            AddForeignKey("dbo.Employees", "Contract_Id", "dbo.Contracts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.Employees", new[] { "Contract_Id" });
            DropPrimaryKey("dbo.TypeEmployeeContracts");
            DropColumn("dbo.Employees", "Contract_Id");
            DropColumn("dbo.Employees", "ContractId");
            AddPrimaryKey("dbo.TypeEmployeeContracts", new[] { "Contract_Id", "TypeEmployee_Id" });
            RenameTable(name: "dbo.TypeEmployeeContracts", newName: "ContractTypeEmployees");
        }
    }
}
