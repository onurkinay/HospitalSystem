using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Appointment
    {
        // Appointment have Doctor and Patient ID
        [Key]
        public int Id { get; set; }


        [ForeignKey("Patient")]
        public int Patient_ID { get; set; }
        [ForeignKey("Doctor")]
        public int Doctor_ID { get; set; }
       
        public string Description { get; set; }
        public double Consultant_Fee { get; set; }
        public DateTime AppointmentDate { get; set; }
         
        public virtual Doctor Doctor { get; set; } 
        public virtual Patient Patient { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}