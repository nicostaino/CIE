namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminQR : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LandUsers", newName: "UserLands");
            DropPrimaryKey("dbo.UserLands");
            CreateTable(
                "dbo.AdminQRs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        QR = c.String(),
                        Active = c.Boolean(nullable: false),
                        LandId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lands", t => t.LandId, cascadeDelete: true)
                .Index(t => t.LandId);
            
            AddPrimaryKey("dbo.UserLands", new[] { "User_Id", "Land_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminQRs", "LandId", "dbo.Lands");
            DropIndex("dbo.AdminQRs", new[] { "LandId" });
            DropPrimaryKey("dbo.UserLands");
            DropTable("dbo.AdminQRs");
            AddPrimaryKey("dbo.UserLands", new[] { "Land_Id", "User_Id" });
            RenameTable(name: "dbo.UserLands", newName: "LandUsers");
        }
    }
}
