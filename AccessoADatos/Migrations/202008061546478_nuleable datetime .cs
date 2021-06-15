namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuleabledatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IngressEgresses", "IngressDateTime", c => c.DateTime());
            AlterColumn("dbo.IngressEgresses", "EgressDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IngressEgresses", "EgressDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IngressEgresses", "IngressDateTime", c => c.DateTime(nullable: false));
        }
    }
}
