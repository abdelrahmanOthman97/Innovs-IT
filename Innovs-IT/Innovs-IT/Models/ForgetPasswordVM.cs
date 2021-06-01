using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Innovs_IT.Models
{
    public class ForgetPasswordVM
    {

        [Required(ErrorMessage = "Required Email")]
        [EmailAddress(ErrorMessage = "You Must Enter Valid Mail")]
        public string Email { get; set; }
    }
}
