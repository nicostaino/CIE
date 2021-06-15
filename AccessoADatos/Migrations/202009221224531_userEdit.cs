namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IngressEgresses", "UserEditID", c => c.Long());
            AddColumn("dbo.IngressEgresses", "EditDateTime", c => c.DateTime());
            AddColumn("dbo.IngressEgresses", "isDeleted", c => c.Boolean(nullable: false));
            CreateIndex("dbo.IngressEgresses", "UserEditID");
            AddForeignKey("dbo.IngressEgresses", "UserEditID", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngressEgresses", "UserEditID", "dbo.Users");
            DropIndex("dbo.IngressEgresses", new[] { "UserEditID" });
            DropColumn("dbo.IngressEgresses", "isDeleted");
            DropColumn("dbo.IngressEgresses", "EditDateTime");
            DropColumn("dbo.IngressEgresses", "UserEditID");
        }
    }
}
