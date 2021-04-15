using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GW_JPS.ViewModels
{
    public class BillPaymentViewModel
    {
        [Required(ErrorMessage = "Enter Account Number"), MaxLength(12)]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
    }
}
