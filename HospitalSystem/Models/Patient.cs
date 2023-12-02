using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Patient
    {
        // patient may be stay in any room

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DOB { get; set; }
        public bool Gender { get; set; }
        public string Blood_Group { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public int Room_ID { get; set; } = -1;
        public virtual Room Room { get; set; }


    }
}