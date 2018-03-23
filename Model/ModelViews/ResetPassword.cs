using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelPr.ModelViews
{
    public class ResetPassword
    {
        [StringLength(50)]
        public string Email { get; set; }
        
        [StringLength(200)]
        public string Password { get; set; }
        
        [NotMapped]
        [Compare("Password")]
        [StringLength(200)]
        public string ConfirmPassword { get; set; }
    }
}
