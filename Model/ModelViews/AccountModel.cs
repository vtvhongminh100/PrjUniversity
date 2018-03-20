using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPr.ModelViews
{

    public class AccountModel
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(30,ErrorMessage = "Username contains only 20 characters.")]
        [DisplayName("Username")]

        public string Username { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Password contains only 20 characters.")]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
