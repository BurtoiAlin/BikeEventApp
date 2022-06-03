using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeEvent.Models
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
    }
}
