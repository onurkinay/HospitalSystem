namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userroles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Doctors", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Patients", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "UserId");
            DropColumn("dbo.Doctors", "UserId");
            DropColumn("dbo.Admins", "UserId");
        }
    }
}
