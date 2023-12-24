namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Age", c => c.Int(nullable: false));
        }
    }
}
