namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregamdoQR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QRs", "RegistrationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.QRs", "FechaAlta");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QRs", "FechaAlta", c => c.DateTime(nullable: false));
            DropColumn("dbo.QRs", "RegistrationDate");
        }
    }
}
