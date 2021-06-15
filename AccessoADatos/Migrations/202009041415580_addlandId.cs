namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlandId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TypeEmployees", "LandId", c => c.Long());
            CreateIndex("dbo.TypeEmployees", "LandId");
            AddForeignKey("dbo.TypeEmployees", "LandId", "dbo.Lands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeEmployees", "LandId", "dbo.Lands");
            DropIndex("dbo.TypeEmployees", new[] { "LandId" });
            DropColumn("dbo.TypeEmployees", "LandId");
        }
    }
}
