using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Models
{
    public class Customer
    {
        //Customer Class 
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Parish { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Country { get; set; }

        [Required, EmailAddress, Display(Name = "Email")] public string userEmail { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password")] public string userPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Comfirmed Password"), Compare("userPassword", ErrorMessage = "Password does not match")] public string userConfirmedPassword { get; set; }
        
        [Required(ErrorMessage = "Required")]
        [Range(1000000, 9999999, ErrorMessage = "Account Number cannot exceed 7digits")]
        public long AccountNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(400100000000, 400199999999, ErrorMessage = "Card Number must begin with 4001 and cannot exceed 12digits")]
        public long CardNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Required")]
        public float Balance { get; set; }
    }
}
