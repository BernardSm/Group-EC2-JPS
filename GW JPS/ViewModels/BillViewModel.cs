using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GW_JPS.ViewModels
{
    public class BillViewModel
    {

        public int PremisesNumber { get; set; }

        public string CustomerId { get; set; }

        [Required]
        [StringLength(155, MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
