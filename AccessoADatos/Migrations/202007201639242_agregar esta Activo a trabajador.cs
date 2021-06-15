namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarestaActivoatrabajador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "isActive");
        }
    }
}
