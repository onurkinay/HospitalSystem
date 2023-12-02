using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Doctor
    {
        // Doctor does have relationship with Department
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set;}
        public bool Gender { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        public double Salary { get; set; }
        public string Specializations { get; set; }
        public string Experience { get; set; }
        public string Languages { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<DateTime> Schedules { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

    }
}