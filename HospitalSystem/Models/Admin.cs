using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Admin
    {
        // admin doesn't have any relationship

        public int Id { get; set; }
        public int Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime DOB { get; set; }
        public bool Accountant { get; set; }

    }
}