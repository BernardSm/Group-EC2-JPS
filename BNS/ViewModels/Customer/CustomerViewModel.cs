using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.ViewModels.Customer
{
    public class CustomerViewModel
    {
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

        [Remote(action: "usedEmail", controller: "Customer"), Required, EmailAddress, Display(Name = "Email")] public string userEmail { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password")] public string userPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Comfirmed Password"), Compare("userPassword", ErrorMessage = "Password does not match")] public string userConfirmedPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        [Remote(action: "usedAccountNumber", controller: "Customer"), Range(1000000, 9999999, ErrorMessage = "Account Number cannot exceed 7digits")]
        public long AccountNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        [Remote(action: "usedCardNumber", controller: "Customer"), Range(400100000000, 400199999999, ErrorMessage = "Card Number must begin with 4001 and cannot exceed 12digits")]
        public long CardNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Required")]
        public float Balance { get; set; }
    }
}
