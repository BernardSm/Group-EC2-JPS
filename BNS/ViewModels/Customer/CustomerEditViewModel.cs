using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.ViewModels.Customer
{
    public class CustomerEditViewModel
    {
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Parish { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Country { get; set; }

        public string userEmail { get; set; }

    }
}
