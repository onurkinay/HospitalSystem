namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeconsfee : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "Consultant_Fee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Consultant_Fee", c => c.Double(nullable: false));
        }
    }
}
