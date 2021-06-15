namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "LandId", c => c.Long());
            CreateIndex("dbo.Contractors", "LandId");
            AddForeignKey("dbo.Contractors", "LandId", "dbo.Lands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contractors", "LandId", "dbo.Lands");
            DropIndex("dbo.Contractors", new[] { "LandId" });
            DropColumn("dbo.Contractors", "LandId");
        }
    }
}
