using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityDao.EF;

namespace UniversityWebApp.Models
{
    public class CategoryIdeaView
    {
        public CategoryGroupIdea CateGr { get; set; }
        public List<IdeaCategoryModelView> lvIdeaCate { get; set; }
        
        public CategoryIdeaView(CategoryGroupIdea lvCateGr, List<IdeaCategoryModelView> lvIdeaCate)
        {
            this.CateGr = CateGr;
            this.lvIdeaCate = lvIdeaCate;
        }
        public CategoryIdeaView()
        {
        }
    }
}
