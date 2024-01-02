namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "PriceUnit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "PriceUnit");
        }
    }
}
