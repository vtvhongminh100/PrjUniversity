using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityDao.EF;

namespace UniversityDao.Dao
{
    public class IdeaCategoryDao
    {
        EF.UniversityDbContext db = null;
        public IdeaCategoryDao()
        {
            db = new EF.UniversityDbContext();
        }
        public List<IdeaCategory> GetAllCateIDByGrId(int grCateId)
        {
            var result = db.IdeaCategories.Where(x => x.IdeaCateStatus == true && x.GroupCateIdea == grCateId).ToList();
            return result;
        }
        public List<IdeaCategory> GetAllCateIDBySt()
        {
            var result = db.IdeaCategories.Where(x => x.IdeaCateStatus == true).ToList();
            return result;
        }
        public List<IdeaCategoryView> GetAllIdeaCategory(int ID)
        {
            var result = (from a in db.CategoryGroupIdeas
                          join b in db.IdeaCategories
                          on a.CategoryGroupIdeaID equals b.GroupCateIdea
                          where b.GroupCateIdea == ID
                          select new IdeaCategoryView()
                          {
                              IdeaCategoryID = b.IdeaCategoryID,
                              CategoryName = b.CategoryName,
                              CategoryDescription = b.CategoryDescription,
                              CreatedBy = b.CreatedBy,
                              CreatedDate = (DateTime)b.CreatedDate,
                              ModifiedDate = (DateTime)b.ModifiedDate,
                              GroupCateIdea = (int)b.GroupCateIdea,
                              IdeaCateViewC = (int)b.IdeaCateViewC,
                              IdeaCateStatus = (bool)b.IdeaCateStatus,
                              ModifiedBy = b.ModifiedBy,
                              FinalCloseDate = b.FinalCloseDate
                          }).ToList();

            return result;
        }
        public bool InsertIdeaCategory(IdeaCategory model)
        {
            try
            {
                model.IdeaCateViewC = 0;
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.FinalCloseDate = DateTime.Now.AddDays(90);
                db.IdeaCategories.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ChkFinaGtThanNow(int ideaCateId)
        {
            var idc = db.IdeaCategories.Find(ideaCateId);
            if (idc.FinalCloseDate >= DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateIdeaCategory(IdeaCategory model)
        {
            try
            {
                var idc = db.IdeaCategories.Find(model.IdeaCategoryID);
                idc.ModifiedDate = DateTime.Now;
                idc.IdeaCateStatus = model.IdeaCateStatus;
                idc.ModifiedBy = model.ModifiedBy;
                idc.GroupCateIdea = model.GroupCateIdea;
                DateTime sqlDate = DateTime.Now;
                idc.FinalCloseDate = Convert.ToDateTime(model.FinalCloseDate.Date.ToString("dd-MM-yyyy"));
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IdeaCategory GetIdeaCateByID(int ID)
        {
            var result = db.IdeaCategories.SingleOrDefault(x => x.IdeaCategoryID == ID);
            return result;

        }
        public List<IdeaCategory> GetIdeaCateByGr(int grId)
        {
            var result = db.IdeaCategories.Where(x => x.IdeaCateStatus == true && x.GroupCateIdea == grId);
            return result.ToList();

        }
        public bool DeleteIdeaCategory(int id)
        {
            try
            {
                var idc = db.IdeaCategories.Find(id);
                db.IdeaCategories.Remove(idc);
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
