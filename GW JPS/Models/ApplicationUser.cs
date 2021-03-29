using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GW_JPS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Range(100000, 999999, ErrorMessage = "Premises number must be 6 digits long and dont begin with a 0.")]
        public int PremisesNumber { get; set; }
    }
}
