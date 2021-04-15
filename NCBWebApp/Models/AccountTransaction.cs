using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCBWebApp.Models
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string CusId { get; set; }
    }
}
