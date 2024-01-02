using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Bill
    { 
        [Key]
        public int Id { get; set; }

        [DisplayName("Issued Date")]
        public DateTime Issued_Date { get; set; }
        public double Amount { get; set; }
        [DisplayName("Is Paid?")]
        public bool IsPaid { get; set; }=false;

        public int Appointment_ID { get; set; }
        public Appointment CurAppointment { get; set; }



         
    }
}