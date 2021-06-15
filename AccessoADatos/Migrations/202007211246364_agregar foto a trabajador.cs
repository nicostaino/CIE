namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarfotoatrabajador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Img", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Img");
        }
    }
}
