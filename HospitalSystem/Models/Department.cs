using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}