namespace UniversityDao.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(20)]
        public string Username { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        [StringLength(20)]
        public string Role { get; set; }

        public bool Status { get; set; }
    }
}
