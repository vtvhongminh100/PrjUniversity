using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPr.ModelViews
{
    public class IdeaCategoryView
    {
        public int IdeaCategoryID { get; set; }

        [StringLength(250)]
        public string CategoryName { get; set; }
        public DateTime? FinalCloseDate { get; set; }

        [StringLength(250)]
        public string CategoryDescription { get; set; }

        public int IdeaCateViewC { get; set; }
        public string Group { get; set; }
        public int GroupCateIdea { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public bool IdeaCateStatus { get; set; }
    }
}
