using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }
    }
}