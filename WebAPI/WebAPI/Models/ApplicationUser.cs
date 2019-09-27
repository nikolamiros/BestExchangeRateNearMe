using GeoAPI.Geometries;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public abstract class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }        
    }

    public class ExchangeOfficer : ApplicationUser
    {
        public IPoint Location { get; set; }

        public string Address { get; set; }


        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; } 
            = new List<ExchangeRate>();
    }

    public class ExchangeCustomer : ApplicationUser {

    }
}
