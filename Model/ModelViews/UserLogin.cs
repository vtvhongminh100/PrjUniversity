using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelPr.ModelViews
{
    public class UserLogin
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Username contains only 20 characters.")]
        [DisplayName("Username")]

        public string Username { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Password contains only 20 characters.")]
        [DisplayName("Password")]
        public string Password { get; set; }

    }
}
