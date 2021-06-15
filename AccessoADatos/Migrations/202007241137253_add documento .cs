namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddocumento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlackLists", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlackLists", "Name");
        }
    }
}
