using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Bill
    {
        //one bill belongs to appointment
        [Key]
        public int Id { get; set; }
        public int Bill_No { get; set; }


        [ForeignKey("Appointment")]
        public int Appointment_ID { get; set; } 

        public DateTime Issued_Date { get; set; }
        public double Amount { get; set; }
        public bool IsPaid { get; set; }=false;


        public virtual Appointment Appointment { get; set; }
    }
}