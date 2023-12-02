using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Bill
    { 
        //one bill belongs to appointment

        public int Id { get; set; }
        public int Bill_No { get; set; }
        public int Apointment_ID { get; set; }
        public DateTime Issued_Date { get; set; }
        public double Amount { get; set; }
        public bool IsPaid { get; set; }=false;
    }
}