using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name = "Email")]
        public string userEmail { get; set; }

        [Required, DataType(DataType.Password),Display(Name ="Password")]
        public string userPassword { get; set; }

        [Display(Name = "Remember me")]
        public bool userRememberMe { get; set; }
    }
}
