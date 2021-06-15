namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6Migracion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lands", "User_Id", "dbo.Users");
            DropIndex("dbo.Lands", new[] { "User_Id" });
            CreateTable(
                "dbo.UserLands",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        Land_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Land_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Lands", t => t.Land_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Land_Id);
            
            DropColumn("dbo.Lands", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lands", "User_Id", c => c.Long());
            DropForeignKey("dbo.UserLands", "Land_Id", "dbo.Lands");
            DropForeignKey("dbo.UserLands", "User_Id", "dbo.Users");
            DropIndex("dbo.UserLands", new[] { "Land_Id" });
            DropIndex("dbo.UserLands", new[] { "User_Id" });
            DropTable("dbo.UserLands");
            CreateIndex("dbo.Lands", "User_Id");
            AddForeignKey("dbo.Lands", "User_Id", "dbo.Users", "Id");
        }
    }
}
