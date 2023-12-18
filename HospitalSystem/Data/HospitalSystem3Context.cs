using HospitalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace HospitalSystem.Data
{
    public class HospitalSystem3Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public HospitalSystem3Context() : base("name=HospitalSystem3Context")
        {
        }

        public System.Data.Entity.DbSet<HospitalSystem.Models.Admin> Admins { get; set; }

        public System.Data.Entity.DbSet<HospitalSystem.Models.Department> Departments { get; set; }

         public System.Data.Entity.DbSet<HospitalSystem.Models.Doctor> Doctors { get; set; }

        public System.Data.Entity.DbSet<HospitalSystem.Models.Patient> Patients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Doctor>()
                .HasRequired<Department>(s => s.CurDepartment)
                .WithMany(g => g.Doctors)
                .HasForeignKey<int>(s => s.CurDeptartmentID);

            modelBuilder.Entity<Appointment>()
                .HasRequired<Doctor>(s => s.Doctor)
                .WithMany(g => g.Appointments)
                .HasForeignKey<int>(s => s.Doctor_ID);

            modelBuilder.Entity<Appointment>()
                .HasRequired<Patient>(s => s.Patient)
                .WithMany(g => g.Appointments)
                .HasForeignKey<int>(s => s.Patient_ID);

            modelBuilder.Entity<Bill>()
                 .HasRequired<Appointment>(s => s.CurAppointment)
                 .WithMany(g => g.Bills)
                 .HasForeignKey<int>(s => s.Appointment_ID);

            modelBuilder.Entity<Prescription>()
                 .HasRequired<Appointment>(s => s.CurAppointment)
                 .WithMany(g => g.Prescriptions)
                 .HasForeignKey<int>(s => s.Appointment_ID);

        }

        public System.Data.Entity.DbSet<HospitalSystem.Models.Appointment> Appointments { get; set; }

        public System.Data.Entity.DbSet<HospitalSystem.Models.Bill> Bills { get; set; }

        public System.Data.Entity.DbSet<HospitalSystem.Models.Prescription> Prescriptions { get; set; }
         
    }
}
