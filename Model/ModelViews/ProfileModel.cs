using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPr.ModelViews {
    public class ProfileModel {
        public int UserID { get; set; }

        public string CurrentPassword { get; set; }

        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

    }
}
