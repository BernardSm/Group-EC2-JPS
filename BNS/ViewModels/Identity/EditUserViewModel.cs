using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
        }   
        [Required] public string userID { get; set; }
        [Required] public string UserName { get; set; }
        [Required, EmailAddress, Display(Name = "Email")] public string userEmail { get; set; }
        [Required] public string userFirstName { get; set; }
        [Required] public string userLastName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
