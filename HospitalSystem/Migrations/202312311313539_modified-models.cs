namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedmodels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Appointments", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Blood_Group", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Phone", c => c.String(nullable: false));
            DropColumn("dbo.Admins", "Username");
            DropColumn("dbo.Admins", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "Password", c => c.String());
            AddColumn("dbo.Admins", "Username", c => c.Int(nullable: false));
            AlterColumn("dbo.Patients", "Phone", c => c.String());
            AlterColumn("dbo.Patients", "Address", c => c.String());
            AlterColumn("dbo.Patients", "Email", c => c.String());
            AlterColumn("dbo.Patients", "Blood_Group", c => c.String());
            AlterColumn("dbo.Patients", "Surname", c => c.String());
            AlterColumn("dbo.Patients", "Name", c => c.String());
            AlterColumn("dbo.Doctors", "Email", c => c.String());
            AlterColumn("dbo.Doctors", "Phone", c => c.String());
            AlterColumn("dbo.Doctors", "Surname", c => c.String());
            AlterColumn("dbo.Doctors", "Name", c => c.String());
            AlterColumn("dbo.Appointments", "Description", c => c.String());
            AlterColumn("dbo.Admins", "Address", c => c.String());
            AlterColumn("dbo.Admins", "Email", c => c.String());
        }
    }
}
