using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebApp.Models
{
    public class IdeaView
    {
        public int IdeaID { get; set; }

        [StringLength(350)]
        public string IdeaTitle { get; set; }
        [Column(TypeName = "ntext")]
        public string IdeaContent { get; set; }

        [StringLength(500)]
        public string IdeaDescription { get; set; }

        public int? IdeaCategory { get; set; }
        public int CommentCount { get; set; }

        [StringLength(200)]
        public string FileSP { get; set; }

        public int? IdeaViewCount { get; set; }

        public bool AllowAnonymous { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public DateTime? ClosedDate { get; set; }

        public bool IdeaStatus { get; set; }
    }
}
