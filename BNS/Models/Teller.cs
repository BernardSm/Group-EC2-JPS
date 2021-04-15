using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Models
{
    public class Teller
    {
        //Dont register Tellers
        [Required(ErrorMessage = "Required")]
        [MaxLength(8)]
        public int TellerId { get; set; }

        [Required(ErrorMessage = "Required")] public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]public string LastName { get; set; }
        [Required, EmailAddress, Display(Name = "Email")] public string userEmail { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password")] public string userPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Comfirmed Password"), Compare("userPassword", ErrorMessage = "Password does not match")] public string userConfirmedPassword { get; set; }
    }
}