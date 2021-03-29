using GW_JPS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GW_JPS.ViewModels
{
    public class UserBillsViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int PremisesNumber { get; set; }

        public List<Bill> Bills { get; set; }
    }
}
