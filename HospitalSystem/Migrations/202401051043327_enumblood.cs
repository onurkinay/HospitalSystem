namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enumblood : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "Blood_Group", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Blood_Group", c => c.String(nullable: false));
        }
    }
}
