namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregandotypeemployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "TypeEnum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "TypeEnum");
        }
    }
}
