using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GW_JPS.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
