namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6tamigracion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Roles", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "RoleId", c => c.Int(nullable: false));
        }
    }
}
