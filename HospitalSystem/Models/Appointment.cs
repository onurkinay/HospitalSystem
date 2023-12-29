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
          
        public string Description { get; set; } 
         
        public DateTime AppointmentDate { get; set; }

        public int Doctor_ID { get; set; }
        public int Patient_ID { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

        public ICollection<Bill> Bills { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }



    }
}