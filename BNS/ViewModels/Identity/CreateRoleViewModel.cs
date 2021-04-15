using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BNS.ViewModels.Identity
{
    public class CreateRoleViewModel
    {
        [Required]
        public string roleName { get; set; }
    }
}
