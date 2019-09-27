using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ApplicationUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Address { get; set; }
    }
}
