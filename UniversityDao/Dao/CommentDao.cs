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
    }
}
