using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityDao.EF;

namespace UniversityDao.Dao
{
    public class CateGrIdeaDao
    {
        EF.UniversityDbContext db = null;
        public CateGrIdeaDao()
        {
            db = new EF.UniversityDbContext();
        }
        public List<CategoryGroupIdea> GetAllGrCateIdea()
        {
            var listGrCateIdeaDao = db.CategoryGroupIdeas.ToList();
        
            return listGrCateIdeaDao;
        }

        public List<CategoryGroupIdea> GetAllGrCIBySt()
        {
            var grCate = db.CategoryGroupIdeas.Where(x => x.CategoryGrIdeaSt == true).ToList();
            return grCate;
        }
        public int InsertGrCateIdea(CategoryGroupIdea model)
        {
                model.CreatedDate = DateTime.Now;
                db.CategoryGroupIdeas.Add(model);
                db.SaveChanges();
                return model.CategoryGroupIdeaID;
        }
        public CategoryGroupIdea GetCateGrIdeaByID(int ID)
        {
            var result = db.CategoryGroupIdeas.SingleOrDefault(x => x.CategoryGroupIdeaID == ID);
            return result;
        }
        public bool UpdateGrCateIdea(CategoryGroupIdea model)
        {
            try
            {
                var gci = db.CategoryGroupIdeas.Find(model.CategoryGroupIdeaID);
                gci.CateGrIdeaDes = model.CateGrIdeaDes;
                gci.CategoryGroupName = model.CategoryGroupName;
                gci.ModifiedDate = DateTime.Now;
                gci.ModifiedBy = model.ModifiedBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteCateGrIdea(int ID)
        {
            try
            {
                var gci = db.CategoryGroupIdeas.Find(ID);
                db.CategoryGroupIdeas.Remove(gci);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
     
        }
    }
}
