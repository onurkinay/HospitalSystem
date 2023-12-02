namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.Int(nullable: false),
                        Password = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        DOB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Consultant_Fee = c.Double(nullable: false),
                        AppointmentDate = c.DateTime(nullable: false),
                        Doctor_ID = c.Int(nullable: false),
                        Patient_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.Doctor_ID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_ID, cascadeDelete: true)
                .Index(t => t.Doctor_ID)
                .Index(t => t.Patient_ID);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Issued_Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        Appointment_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appointments", t => t.Appointment_ID, cascadeDelete: true)
                .Index(t => t.Appointment_ID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Age = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Salary = c.Double(nullable: false),
                        Specializations = c.String(),
                        Experience = c.String(),
                        Languages = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        CurDeptartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.CurDeptartmentID, cascadeDelete: true)
                .Index(t => t.CurDeptartmentID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Blood_Group = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        CurRoom_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.CurRoom_ID)
                .Index(t => t.CurRoom_ID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Medicinde = c.String(),
                        Remark = c.String(),
                        Advice = c.String(),
                        Appointment_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appointments", t => t.Appointment_ID, cascadeDelete: true)
                .Index(t => t.Appointment_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prescriptions", "Appointment_ID", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "Patient_ID", "dbo.Patients");
            DropForeignKey("dbo.Patients", "CurRoom_ID", "dbo.Rooms");
            DropForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "CurDeptartmentID", "dbo.Departments");
            DropForeignKey("dbo.Bills", "Appointment_ID", "dbo.Appointments");
            DropIndex("dbo.Prescriptions", new[] { "Appointment_ID" });
            DropIndex("dbo.Patients", new[] { "CurRoom_ID" });
            DropIndex("dbo.Doctors", new[] { "CurDeptartmentID" });
            DropIndex("dbo.Bills", new[] { "Appointment_ID" });
            DropIndex("dbo.Appointments", new[] { "Patient_ID" });
            DropIndex("dbo.Appointments", new[] { "Doctor_ID" });
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Rooms");
            DropTable("dbo.Patients");
            DropTable("dbo.Departments");
            DropTable("dbo.Doctors");
            DropTable("dbo.Bills");
            DropTable("dbo.Appointments");
            DropTable("dbo.Admins");
        }
    }
}
