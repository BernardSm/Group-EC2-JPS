using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NCBWebApp.ViewModels
{
    public class CustomerDepositViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address"), MaxLength(40)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Enter the amount you wish to withdraw")]
        [DataType(DataType.Currency)]
        public decimal DepositAmount { get; set; }

        [Required(ErrorMessage = "Enter Account Number"), MaxLength(7)]
        [RegularExpression("^[0-9]{7}", ErrorMessage = "Enter a 7 digit account number")]
        [DataType(DataType.CreditCard)]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Enter Account Number"), MaxLength(12)]
        [DataType(DataType.CreditCard)]
        [RegularExpression("^(9505)[0-9]{8}", ErrorMessage = "Enter a 12 digit card number starting with 9505")]
        public string CardNumber { get; set; }

        public string AccountType { get; set; }
    }
}
