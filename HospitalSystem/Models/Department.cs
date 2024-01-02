using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace HospitalSystem.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Department Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Price Per Unit")]
        public double PriceUnit { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}