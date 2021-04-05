using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_NBC.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address"), MaxLength(40)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Balance of $5000 or more")]
        [Range(5000, 10000, ErrorMessage = "Balance must be between $5000 and $10000")]
        [DataType(DataType.Currency)]
        public float Balance { get; set; }

        [Required(ErrorMessage = "Enter Account Number"),MaxLength(7)]
        [RegularExpression(@"^[0-9]{7}", ErrorMessage = "Enter a 7 digit number")]
        [DataType(DataType.CreditCard)]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Enter Account Number"),MaxLength(12)]
        [DataType(DataType.CreditCard)]
        [RegularExpression(@"^[9505]+[0-9]{11}", ErrorMessage = "Enter a 12 digit card number starting with 9505")]
        public string CardNumber { get; set; }

        public string AccountType { get; set; }
    }
}
