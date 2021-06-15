namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsContractorToEmployee : DbMigration
    {
        public override void Up()
        {
          
            AddColumn("dbo.Employees", "ContractorId", c => c.Long());
            CreateIndex("dbo.Employees", "ContractorId");
            AddForeignKey("dbo.Employees", "ContractorId", "dbo.Contractors", "Id");
  
 
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Employees", new[] { "ContractorId" });
            DropColumn("dbo.Employees", "ContractorId");
           
        }
    }
}
