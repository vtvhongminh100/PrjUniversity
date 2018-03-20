namespace UniversityDao.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IdeaCategory")]
    public partial class IdeaCategory
    {
        public int IdeaCategoryID { get; set; }

        [StringLength(250)]
        public string CategoryName { get; set; }

        [StringLength(250)]
        public string CategoryDescription { get; set; }

        public int? IdeaCateViewC { get; set; }

        public int? GroupCateIdea { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime FinalCloseDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public bool IdeaCateStatus { get; set; }
    }
}
