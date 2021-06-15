namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_contractid_ingressegress : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IngressEgresses", "ContractId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IngressEgresses", "ContractId", c => c.Int(nullable: false));
        }
    }
}
