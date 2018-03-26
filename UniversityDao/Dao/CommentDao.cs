using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityDao.EF;

namespace UniversityDao.Dao
{
    public class CommentDao
    {
        UniversityDbContext db = new UniversityDbContext();

        

        public bool InsertComment(Comment model,int parentId)
        {
            try
            {
                model.CmStatus = true;
                model.CmParentId = parentId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                db.Comments.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public bool UpdateComment(Comment model)
        //{
        //    try
        //    {
        //        var com = db.Comments.Find(model.CommentID);
        //        com.ModifiedDate = DateTime.Now;
        //        com.Description = model.Description;
        //        com.Emotion = model.Emotion;
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public bool DeleteComment(int id)
        {
            try
            {
                var com = db.Comments.Find(id);
                db.Comments.Remove(com);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Comment> GetCmByIdeaId(int ideaId)
        {
            var result = db.Comments.Where(x => x.CmParentId == ideaId && x.CmStatus == true).ToList();
            return result;
        }
        public List<Comment> GetCmByIdeaIdForStu(int ideaId)
        {
            var rs = (from a in db.Comments join b in db.Ideas
                     on a.CmParentId equals b.IdeaID
                     join c in db.Accounts on a.CreatedBy equals c.Username
                     where (c.Role.Equals("STU") && b.IdeaID == ideaId)
                     select new  {
                         CmContent = a.CmContent,
                         CmParentId = a.CmParentId,
                         CreatedDate = a.CreatedDate,
                         ModifiedDate = a.ModifiedDate,
                         CreatedBy = a.CreatedBy,
                         ModifiedBy = a.ModifiedBy
                     }).ToList().Select(x => new Comment {
                         CmContent = x.CmContent,
                         CmParentId = x.CmParentId,
                         CreatedDate = x.CreatedDate,
                         ModifiedDate = x.ModifiedDate,
                         CreatedBy = x.CreatedBy,
                         ModifiedBy = x.ModifiedBy
                     }).ToList();

            return rs;
        }

    }
}
