namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatelasupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "lastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "lastUpdate");
        }
    }
}
