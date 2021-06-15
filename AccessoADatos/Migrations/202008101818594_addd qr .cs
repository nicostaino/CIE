namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddqr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "QR", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "QR");
        }
    }
}
