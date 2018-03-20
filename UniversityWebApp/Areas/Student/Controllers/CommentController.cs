using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityDao.Dao;
using UniversityDao.EF;

namespace UniversityWebApp.Areas.Student.Controllers
{
    public class CommentController : Controller
    {
        // GET: Student/Comment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InsertComment(Comment model,int ideaId)
        {
            try
            {
                model.CreatedBy = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).Username;
                new CommentDao().InsertComment(model,ideaId);
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

      
    }
}