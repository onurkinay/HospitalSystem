﻿namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Accountant = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
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
                        UserId = c.String(),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Salary = c.Double(nullable: false),
                        Specializations = c.String(),
                        Experience = c.String(),
                        Languages = c.String(),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
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
                        Name = c.String(nullable: false),
                        PriceUnit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Blood_Group = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(),
                        Phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Appointments", "Doctor_ID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "CurDeptartmentID", "dbo.Departments");
            DropForeignKey("dbo.Bills", "Appointment_ID", "dbo.Appointments");
            DropIndex("dbo.Prescriptions", new[] { "Appointment_ID" });
            DropIndex("dbo.Doctors", new[] { "CurDeptartmentID" });
            DropIndex("dbo.Bills", new[] { "Appointment_ID" });
            DropIndex("dbo.Appointments", new[] { "Patient_ID" });
            DropIndex("dbo.Appointments", new[] { "Doctor_ID" });
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Patients");
            DropTable("dbo.Departments");
            DropTable("dbo.Doctors");
            DropTable("dbo.Bills");
            DropTable("dbo.Appointments");
            DropTable("dbo.Admins");
        }
    }
}
