using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCB.Models
{
    public class mvcTellerModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Age")]
        public Nullable<int> Age { get; set; }

        [Required(ErrorMessage = "Enter UserName")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
    }
}