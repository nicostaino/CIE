namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIngressegress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IngressEgresses", "ForemanId", c => c.Long());
            AddColumn("dbo.IngressEgresses", "UserIdIngressRegister", c => c.Long());
            CreateIndex("dbo.IngressEgresses", "ForemanId");
            CreateIndex("dbo.IngressEgresses", "UserIdIngressRegister");
            AddForeignKey("dbo.IngressEgresses", "ForemanId", "dbo.Employees", "Id");
            AddForeignKey("dbo.IngressEgresses", "UserIdIngressRegister", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngressEgresses", "UserIdIngressRegister", "dbo.Users");
            DropForeignKey("dbo.IngressEgresses", "ForemanId", "dbo.Employees");
            DropIndex("dbo.IngressEgresses", new[] { "UserIdIngressRegister" });
            DropIndex("dbo.IngressEgresses", new[] { "ForemanId" });
            DropColumn("dbo.IngressEgresses", "UserIdIngressRegister");
            DropColumn("dbo.IngressEgresses", "ForemanId");
        }
    }
}
