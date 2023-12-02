using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Prescription
    {
        // prescription will only be issued when there is an appointment(1-1)
        //

        public int Id { get; set; }

        public int Appointment_ID { get; set; }
        public Appointment Appointment { get; set; }


        public string Medicinde { get; set; }
        public string Remark { get; set; }
        public string Advice { get; set; }
    }
}