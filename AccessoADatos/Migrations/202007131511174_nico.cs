namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QRs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Codigo = c.String(),
                        FechaAlta = c.DateTime(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
          
            AddColumn("dbo.Employees", "Sex", c => c.String());
            AddColumn("dbo.Employees", "Local", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "isforeman", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            
            DropForeignKey("dbo.QRs", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.QRs", new[] { "EmployeeId" });
            DropColumn("dbo.Employees", "isforeman");
            DropColumn("dbo.Employees", "Local");
            DropColumn("dbo.Employees", "Sex");
            DropTable("dbo.QRs");
        }
    }
}
