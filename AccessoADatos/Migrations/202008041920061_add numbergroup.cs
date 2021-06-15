namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnumbergroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "groupNumber", c => c.Long(nullable: false));
            AddColumn("dbo.IngressEgresses", "groupNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IngressEgresses", "groupNumber");
            DropColumn("dbo.Employees", "groupNumber");
        }
    }
}
