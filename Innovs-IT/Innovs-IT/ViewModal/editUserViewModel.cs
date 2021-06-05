using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EM.ViewModal
{
    public class editUserViewModel
    {
        public editUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();


        }
        public string Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name ="User Name")]
        public string userName { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}
