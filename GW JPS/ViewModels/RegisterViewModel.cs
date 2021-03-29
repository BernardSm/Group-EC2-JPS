using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GW_JPS.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The passwords provided do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Range(100000, 999999, ErrorMessage = "Premises number must be 6 digits long and dont begin with a 0.")]
        [Remote(action: "IsPremisesNumberInUse", controller: "Account")]
        public int PremisesNumber { get; set; }
    }
}
