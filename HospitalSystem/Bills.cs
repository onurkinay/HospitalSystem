//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bills
    {
        public int Id { get; set; }
        public System.DateTime Issued_Date { get; set; }
        public double Amount { get; set; }
        public bool IsPaid { get; set; }
        public int Appointment_ID { get; set; }
    
        public virtual Appointments Appointments { get; set; }
    }
}
