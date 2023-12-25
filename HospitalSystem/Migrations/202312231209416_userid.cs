namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "UserId", c => c.String());
            AlterColumn("dbo.Doctors", "UserId", c => c.String());
            AlterColumn("dbo.Patients", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Doctors", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Admins", "UserId", c => c.Int(nullable: false));
        }
    }
}
