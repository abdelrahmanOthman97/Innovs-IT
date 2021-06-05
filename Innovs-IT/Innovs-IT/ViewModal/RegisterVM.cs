using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EM.ViewModal
{
    public class RegisterVM
    {
        [Required(ErrorMessage = " Email is Required"), Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required"), Display(Name = "Password"), DataType(DataType.Password), MinLength(6, ErrorMessage = "Password Must Be At Least 6 Characters")]
        public string Password { get; set; }
        [ System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password Doesn't Match"), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public  IFormFile Image { get; set; }
    }
}
