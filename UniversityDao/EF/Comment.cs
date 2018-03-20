namespace UniversityDao.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Comment")]
    public partial class Comment
    {
        public int CommentId { get; set; }

        public int? CmParentId { get; set; }
        [AllowHtml]
        public string CmContent { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public bool CmStatus { get; set; }
    }
}
