namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFeacturedEventss : DbMigration
    {
        public override void Up()
        {
           // AddColumn("dbo.IngressEgresses", "isForeman", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
       //     DropColumn("dbo.IngressEgresses", "isForeman");
        }
    }
}
