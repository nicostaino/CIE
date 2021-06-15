namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5tamigracion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "IsDeletedByAdmin");
            DropColumn("dbo.Lands", "IsDeletedByAdmin");
            DropColumn("dbo.Users", "IsDeletedByAdmin");
            DropColumn("dbo.Roles", "IsDeletedByAdmin");
            DropColumn("dbo.Temperatures", "IsDeletedByAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Temperatures", "IsDeletedByAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Roles", "IsDeletedByAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsDeletedByAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Lands", "IsDeletedByAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "IsDeletedByAdmin", c => c.Boolean(nullable: false));
        }
    }
}
