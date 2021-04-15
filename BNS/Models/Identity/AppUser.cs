using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "First Name")] public string userFirstName { get; set; }
        [Display(Name = "Last Name")] public string userLastName { get; set; }
        [Display(Name = "Street Name")] public string userStreetName { get; set; }
        [Display(Name = "City")] public string userCity { get; set; }
        [Display(Name = "Parish")] public string userParish { get; set; }
        [Display(Name = "Country")] public string userCountry { get; set; }
    }
}