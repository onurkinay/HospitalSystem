using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Patient
    { 
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surame is required")]
        public string Surname { get; set; }
        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DOB { get; set; }
        [Required]
        public bool Gender { get; set; } 
        [Required(ErrorMessage = "Blood Group is required")]
        [DisplayName("Blood Type")]
        public Blood Blood_Group { get; set; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$",ErrorMessage ="Email is not valid")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        [Required]
        public string Phone { get; set; }
        
          

        public ICollection<Appointment> Appointments { get; set; }

        public enum Blood
        {
            [Display(Name ="A Rh+")]
            ARhPlus,

            [Display(Name = "B Rh+")]
            BRhPlus,

            [Display(Name = "0 Rh+")]
            ZRhPlus,

            [Display(Name = "A Rh-")]
            ARhNegative,

            [Display(Name = "B Rh-")]
            BRhNegative,

            [Display(Name = "0 Rh-")]
            ZRhNegative

        }

    }
}