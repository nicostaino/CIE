namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIngressegresss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "birthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.IngressEgresses", "DocumentEmployee", c => c.Long());
            AddColumn("dbo.IngressEgresses", "DocumentForeman", c => c.Long());
            AddColumn("dbo.IngressEgresses", "TypeEmployeeId", c => c.Long());
            AddColumn("dbo.IngressEgresses", "constratista_Id", c => c.Long());
            CreateIndex("dbo.IngressEgresses", "TypeEmployeeId");
            CreateIndex("dbo.IngressEgresses", "constratista_Id");
            AddForeignKey("dbo.IngressEgresses", "constratista_Id", "dbo.Contractors", "Id");
            AddForeignKey("dbo.IngressEgresses", "TypeEmployeeId", "dbo.TypeEmployees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngressEgresses", "TypeEmployeeId", "dbo.TypeEmployees");
            DropForeignKey("dbo.IngressEgresses", "constratista_Id", "dbo.Contractors");
            DropIndex("dbo.IngressEgresses", new[] { "constratista_Id" });
            DropIndex("dbo.IngressEgresses", new[] { "TypeEmployeeId" });
            DropColumn("dbo.IngressEgresses", "constratista_Id");
            DropColumn("dbo.IngressEgresses", "TypeEmployeeId");
            DropColumn("dbo.IngressEgresses", "DocumentForeman");
            DropColumn("dbo.IngressEgresses", "DocumentEmployee");
            DropColumn("dbo.Employees", "birthDate");
        }
    }
}
