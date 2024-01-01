using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Admin
    {
        // admin doesn't have any relationship
        [Key]
        public int Id { get; set; } 
        public string UserId { get; set; }
        [Required(ErrorMessage = "E-mail is required")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime DOB { get; set; }
        public bool Accountant { get; set; }

    }
}