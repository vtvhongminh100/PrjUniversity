using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPr.ModelViews
{
    public class CategoryGroupIdeaView
    {
        public int CategoryGroupIdeaID { get; set; }

        [StringLength(250)]
        public string GroupCategoryName { get; set; }

        [StringLength(250)]
        public string CateGrIdeaDes { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        public bool CategoryGrIdeaSt { get; set; }
    }
}
