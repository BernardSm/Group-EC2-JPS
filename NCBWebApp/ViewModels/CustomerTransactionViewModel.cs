using NCBWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NCBWebApp.ViewModels
{
    public class CustomerTransactionViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public IList<AccountTransaction> Transactions { get; set; }
    }
}
