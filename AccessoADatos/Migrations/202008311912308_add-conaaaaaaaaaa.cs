namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addconaaaaaaaaaa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeaturedEvents", "CurrentForemanId", c => c.Long());
            DropColumn("dbo.FeaturedEvents", "CurrentForemanId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeaturedEvents", "CurrentForemanId", c => c.Long());
        }
    }
}
