namespace UniversityDao.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryGroupIdea")]
    public partial class CategoryGroupIdea
    {
        public int CategoryGroupIdeaID { get; set; }

        [StringLength(250)]
        public string CategoryGroupName { get; set; }

        [StringLength(250)]
        public string CateGrIdeaDes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public bool CategoryGrIdeaSt { get; set; }
    }
}
