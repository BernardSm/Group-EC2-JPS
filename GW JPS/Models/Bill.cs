using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GW_JPS.Models
{
    public class Bill
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime GenerationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        public int PremisesNumber { get; set; }

        [Required]
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }

        [Required]
        [StringLength(155, MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [StringLength(4)]
        public string Status { get; set; }
    }
}
