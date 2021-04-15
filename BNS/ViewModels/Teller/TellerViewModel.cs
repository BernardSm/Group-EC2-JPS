using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.ViewModels.Teller
{
    public class TellerViewModel
    {
        [Required, Display(Name = "First Name")] public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")] public string LastName { get; set; }
        [Required, EmailAddress, Display(Name = "Email")] public string userEmail { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password")] public string userPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Comfirmed Password"), Compare("userPassword", ErrorMessage = "Password does not match")] public string userConfirmedPassword { get; set; }
    }
}
