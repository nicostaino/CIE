namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nico : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserLands", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserLands", "Land_Id", "dbo.Lands");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employees", "LandId", "dbo.Lands");
            DropForeignKey("dbo.Temperatures", "UserId", "dbo.Users");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Temperatures", new[] { "EmployeeId" });
            DropIndex("dbo.Temperatures", new[] { "LandId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.UserLands", new[] { "User_Id" });
            DropIndex("dbo.UserLands", new[] { "Land_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            RenameColumn(table: "dbo.Lands", name: "LandId", newName: "Employee_Id");
            CreateTable(
                "dbo.BlackLists",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        AdmissionDate = c.DateTime(nullable: false),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.QRs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Codigo = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IngressEgresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RetiredBefore = c.Boolean(nullable: false),
                        DescriptionOut = c.String(),
                        IngressDateTime = c.DateTime(nullable: false),
                        EgressDateTime = c.DateTime(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        LandId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        IsSyncToVisma = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Lands", t => t.LandId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.EmployeeId)
                .Index(t => t.LandId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Employees", "Name", c => c.String());
            AddColumn("dbo.Employees", "LastName", c => c.String());
            AddColumn("dbo.Employees", "DNI", c => c.String());
            AddColumn("dbo.Employees", "ImageBytes", c => c.Binary());
            AddColumn("dbo.Employees", "Sex", c => c.String());
            AddColumn("dbo.Employees", "Local", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "TypeEmployee", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "IsForeman", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "QRHistory_Id", c => c.Long(nullable: false));
            AddColumn("dbo.Lands", "User_Id", c => c.Long(nullable: false));
            AddColumn("dbo.Users", "Land_Id", c => c.Long(nullable: false));
            AddColumn("dbo.Users", "Role_Id", c => c.Long());
            AddColumn("dbo.Users", "Roles_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Temperatures", "EmployeeId", c => c.Long(nullable: false));
            AlterColumn("dbo.Temperatures", "LandId", c => c.Long(nullable: false));
            CreateIndex("dbo.Employees", "QRHistory_Id");
            CreateIndex("dbo.Lands", "Employee_Id");
            CreateIndex("dbo.Lands", "User_Id");
            CreateIndex("dbo.Users", "Land_Id");
            CreateIndex("dbo.Users", "Role_Id");
            CreateIndex("dbo.Users", "Roles_Id");
            CreateIndex("dbo.Temperatures", "EmployeeId");
            CreateIndex("dbo.Temperatures", "LandId");
            AddForeignKey("dbo.Users", "Land_Id", "dbo.Lands", "Id");
            AddForeignKey("dbo.Users", "Role_Id", "dbo.Roles", "Id");
            AddForeignKey("dbo.Users", "Roles_Id", "dbo.Roles", "Id");
            AddForeignKey("dbo.Lands", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Employees", "QRHistory_Id", "dbo.QRs", "Id");
            AddForeignKey("dbo.Employees", "LandId", "dbo.Lands", "Id");
            AddForeignKey("dbo.Lands", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Temperatures", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.Employees", "Nombre");
            DropColumn("dbo.Employees", "Documento");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.UserLands");
            DropTable("dbo.RoleUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Long(nullable: false),
                        User_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id });
            
            CreateTable(
                "dbo.UserLands",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        Land_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Land_Id });
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "Documento", c => c.String());
            AddColumn("dbo.Employees", "Nombre", c => c.String());
            DropForeignKey("dbo.Temperatures", "UserId", "dbo.Users");
            DropForeignKey("dbo.Lands", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "LandId", "dbo.Lands");
            DropForeignKey("dbo.IngressEgresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.IngressEgresses", "LandId", "dbo.Lands");
            DropForeignKey("dbo.IngressEgresses", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.BlackLists", "UserId", "dbo.Users");
            DropForeignKey("dbo.BlackLists", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "QRHistory_Id", "dbo.QRs");
            DropForeignKey("dbo.QRs", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Lands", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "Land_Id", "dbo.Lands");
            DropIndex("dbo.Temperatures", new[] { "LandId" });
            DropIndex("dbo.Temperatures", new[] { "EmployeeId" });
            DropIndex("dbo.IngressEgresses", new[] { "UserId" });
            DropIndex("dbo.IngressEgresses", new[] { "LandId" });
            DropIndex("dbo.IngressEgresses", new[] { "EmployeeId" });
            DropIndex("dbo.QRs", new[] { "EmployeeId" });
            DropIndex("dbo.Users", new[] { "Roles_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "Land_Id" });
            DropIndex("dbo.Lands", new[] { "User_Id" });
            DropIndex("dbo.Lands", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "QRHistory_Id" });
            DropIndex("dbo.BlackLists", new[] { "EmployeeId" });
            DropIndex("dbo.BlackLists", new[] { "UserId" });
            AlterColumn("dbo.Temperatures", "LandId", c => c.Long());
            AlterColumn("dbo.Temperatures", "EmployeeId", c => c.Long());
            DropColumn("dbo.Users", "Roles_Id");
            DropColumn("dbo.Users", "Role_Id");
            DropColumn("dbo.Users", "Land_Id");
            DropColumn("dbo.Lands", "User_Id");
            DropColumn("dbo.Employees", "QRHistory_Id");
            DropColumn("dbo.Employees", "IsActive");
            DropColumn("dbo.Employees", "IsForeman");
            DropColumn("dbo.Employees", "TypeEmployee");
            DropColumn("dbo.Employees", "Local");
            DropColumn("dbo.Employees", "Sex");
            DropColumn("dbo.Employees", "ImageBytes");
            DropColumn("dbo.Employees", "DNI");
            DropColumn("dbo.Employees", "LastName");
            DropColumn("dbo.Employees", "Name");
            DropTable("dbo.IngressEgresses");
            DropTable("dbo.Contractors");
            DropTable("dbo.QRs");
            DropTable("dbo.BlackLists");
            RenameColumn(table: "dbo.Lands", name: "Employee_Id", newName: "LandId");
            CreateIndex("dbo.RoleUsers", "User_Id");
            CreateIndex("dbo.RoleUsers", "Role_Id");
            CreateIndex("dbo.UserLands", "Land_Id");
            CreateIndex("dbo.UserLands", "User_Id");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Temperatures", "LandId");
            CreateIndex("dbo.Temperatures", "EmployeeId");
            CreateIndex("dbo.AspNetUserRoles", "RoleId");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetRoles", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("dbo.Temperatures", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "LandId", "dbo.Lands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserLands", "Land_Id", "dbo.Lands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserLands", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
