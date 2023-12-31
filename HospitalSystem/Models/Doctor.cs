using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Doctor
    {
        // Doctor does have relationship with Department
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DOB { get; set;}
        public bool Gender { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public double Salary { get; set; }
        public string Specializations { get; set; }
        public string Experience { get; set; }
        public string Languages { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; }


        public int CurDeptartmentID { get; set; } 
        public Department CurDepartment { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
       
        
    }
}