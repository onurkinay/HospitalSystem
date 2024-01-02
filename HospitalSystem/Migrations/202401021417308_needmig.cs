namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class needmig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "PhoneNumber", c => c.String());
        }
    }
}
