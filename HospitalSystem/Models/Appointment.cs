﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Appointment
    {
        // Appointment have Doctor and Patient ID
        public int Id { get; set; }
        public int Patient_ID { get; set; }
        public int Doctor_ID { get; set; }
        public string Description { get; set; }
        public double Consultant_Fee { get; set; }
        public DateTime AppointmentDate { get; set; }
        
    }
}