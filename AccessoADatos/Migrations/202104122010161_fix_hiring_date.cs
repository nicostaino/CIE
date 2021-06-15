namespace AccessoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_hiring_date : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "hiringDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "hiringDate", c => c.DateTime(nullable: false));
        }
    }
}
