namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7Migracion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Temperatures", "LandId", c => c.Long());
            CreateIndex("dbo.Temperatures", "LandId");
            AddForeignKey("dbo.Temperatures", "LandId", "dbo.Lands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Temperatures", "LandId", "dbo.Lands");
            DropIndex("dbo.Temperatures", new[] { "LandId" });
            DropColumn("dbo.Temperatures", "LandId");
        }
    }
}
