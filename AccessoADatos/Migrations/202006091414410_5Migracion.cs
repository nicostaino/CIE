namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5Migracion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lands", "User_Id", c => c.Long());
            CreateIndex("dbo.Lands", "User_Id");
            AddForeignKey("dbo.Lands", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lands", "User_Id", "dbo.Users");
            DropIndex("dbo.Lands", new[] { "User_Id" });
            DropColumn("dbo.Lands", "User_Id");
        }
    }
}
