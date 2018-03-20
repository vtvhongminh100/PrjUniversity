namespace UniversityWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class IdeaCategoryModelView
    {
        public int IdeaCategoryID { get; set; }

        [StringLength(250)]
        public string CategoryName { get; set; }

        [StringLength(250)]
        public string CategoryDescription { get; set; }
        public string LatestPost { get; set; }
        public int IdLatestPost { get; set; }
        public int? IdeaCateViewC { get; set; }


        public int CountIdea { get; set; }
        public int? GroupCateIdea { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public bool IdeaCateStatus { get; set; }
    }
}
