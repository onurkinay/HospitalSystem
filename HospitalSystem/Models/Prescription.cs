﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Prescription
    {
        // prescription will only be issued when there is an appointment(1-1)
        //
        [Key]
        public int Id { get; set; } 

        [ForeignKey("Appointment")]
        public int Appointment_ID { get; set; }

        public string Medicinde { get; set; }
        public string Remark { get; set; }
        public string Advice { get; set; } 

        public virtual Appointment Appointment { get; set; }

    }
}