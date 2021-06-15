namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Apellido", c => c.String());
            AddColumn("dbo.Employees", "hiringDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "hiringDate");
            DropColumn("dbo.Employees", "Apellido");
        }
    }
}
