namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcona : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FeaturedEvents", "CurrentForemanId", "dbo.Employees");
            DropColumn("dbo.FeaturedEvents", "CurrentContractorId");
            RenameColumn(table: "dbo.FeaturedEvents", name: "CurrentForemanId", newName: "CurrentContractorId");
            RenameIndex(table: "dbo.FeaturedEvents", name: "IX_CurrentForemanId", newName: "IX_CurrentContractorId");
            AddColumn("dbo.FeaturedEvents", "IdCurrentForeman", c => c.Long());
            CreateIndex("dbo.FeaturedEvents", "IdCurrentForeman");
            AddForeignKey("dbo.FeaturedEvents", "IdCurrentForeman", "dbo.Employees", "Id");
          
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeaturedEvents", "IdCurrentForeman", "dbo.Employees");
            DropIndex("dbo.FeaturedEvents", new[] { "IdCurrentForeman" });
            DropColumn("dbo.FeaturedEvents", "IdCurrentForeman");
            RenameIndex(table: "dbo.FeaturedEvents", name: "IX_CurrentContractorId", newName: "IX_CurrentForemanId");
            RenameColumn(table: "dbo.FeaturedEvents", name: "CurrentContractorId", newName: "CurrentForemanId");
            AddColumn("dbo.FeaturedEvents", "CurrentContractorId", c => c.Long());
            AddForeignKey("dbo.FeaturedEvents", "CurrentForemanId", "dbo.Employees", "Id");
        }
    }
}
