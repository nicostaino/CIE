namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTypeEmployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeEmployees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "TypeEmployee_Id", c => c.Long());
            CreateIndex("dbo.Employees", "TypeEmployee_Id");
            AddForeignKey("dbo.Employees", "TypeEmployee_Id", "dbo.TypeEmployees", "Id");
            DropColumn("dbo.Employees", "TypeEnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "TypeEnum", c => c.Int(nullable: false));
            DropForeignKey("dbo.Employees", "TypeEmployee_Id", "dbo.TypeEmployees");
            DropIndex("dbo.Employees", new[] { "TypeEmployee_Id" });
            DropColumn("dbo.Employees", "TypeEmployee_Id");
            DropTable("dbo.TypeEmployees");
        }
    }
}
