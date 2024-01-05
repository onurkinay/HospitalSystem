using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

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

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Blood
        {
            [Display(Name ="A Rh+")]
            [EnumMember(Value = "A Rh+")]
            [JsonProperty(PropertyName = "A Rh+")]
            ARhPlus,

            [EnumMember(Value = "B Rh+")]
            [Display(Name = "B Rh+")]
            [Description("Blanched Almond Color")]
            [JsonProperty(PropertyName = "B Rh+")]
            BRhPlus,

            [EnumMember(Value = "0 Rh+")]
            [Display(Name = "0 Rh+")] 
            ZRhPlus,

            [EnumMember(Value = "A Rh-")]
            [Display(Name= "A Rh-")]
            [JsonProperty(PropertyName = "A Rh-")]
            ARhNegative,

            [EnumMember(Value = "B Rh-")]
            [Display(Name = "B Rh-")]
            [JsonProperty(PropertyName = "B Rh-")] 
            BRhNegative,

            [EnumMember(Value = "0 Rh-")]
            [Display(Name = "0 Rh-")]
            [JsonProperty(PropertyName = "0 Rh-")]
            ZRhNegative

        }

    }
}