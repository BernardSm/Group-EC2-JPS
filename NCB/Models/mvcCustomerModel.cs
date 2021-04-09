using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCB.Models
{
    public class mvcCustomerModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please Enter Customer Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address"), MaxLength(40)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Balance of $5000 or more")]
        [Range(5000, 10000, ErrorMessage = "Balance must be between $5000 and $10000")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> Balance { get; set; }

        [Required(ErrorMessage = "Enter Account Number"), MaxLength(7)]
        [RegularExpression(@"^[0-9]{7}", ErrorMessage = "Enter a 7 digit number")]
        public Nullable<int> AccountNumber { get; set; }

        [Required(ErrorMessage = "Enter Account Number"), MaxLength(12)]
        [DataType(DataType.CreditCard)]
        [RegularExpression(@"^[9505]+[0-9]{11}", ErrorMessage = "Enter a 12 digit card number starting with 9505")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Enter Type of Account")]
        [DataType(DataType.Text)]
        public string AccountType { get; set; }
    }
}