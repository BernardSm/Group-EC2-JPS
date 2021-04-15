using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        [Display(Name = "Account No.")] public long AccountNum { get; set; }
        [Display(Name = "Customer ID")] public int customerID { get; set; }
        [Display(Name = "Customer Name")] public string customerName { get; set; }
        [Display(Name = "Email")] public string customerEmail { get; set; }
        [Display(Name = "Date of Transaction")] public DateTime TransactionDate { get; set; }
        [Display(Name = "Type of Transaction")] public string TransactionType { get; set; }
        [Display(Name = "Amount")] public float TransactionAmount { get; set; }
    }
}
