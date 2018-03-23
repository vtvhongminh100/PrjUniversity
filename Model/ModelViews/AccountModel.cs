using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [NotMapped]
        [Compare("Password")]
        [StringLength(200)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }

        public bool? EmailConfirmed { get; set; }

        public string Token { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Role { get; set; }

        public bool? Status { get; set; }
    }
}
