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
    
    public partial class Appointments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointments()
        {
            this.Bills = new HashSet<Bills>();
            this.Prescriptions = new HashSet<Prescriptions>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public double Consultant_Fee { get; set; }
        public System.DateTime AppointmentDate { get; set; }
        public int Doctor_ID { get; set; }
        public int Patient_ID { get; set; }
    
        public virtual Doctors Doctors { get; set; }
        public virtual Patients Patients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bills> Bills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prescriptions> Prescriptions { get; set; }
    }
}
