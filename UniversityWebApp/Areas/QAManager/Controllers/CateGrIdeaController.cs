using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityDao.Dao;
using UniversityDao.EF;

namespace UniversityWebApp.Areas.QAManager.Controllers
{
    public class CateGrIdeaController : BaseController
    {
        // GET: QAManager/GrCateIdea
        public ActionResult Index()
        {
            var result = new CateGrIdeaDao().GetAllGrCateIdea();
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryGroupIdea model)
        {
            model.CreatedBy = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).Username;
            var result = new CateGrIdeaDao().InsertGrCateIdea(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int ID)
        {
            var result = new CateGrIdeaDao().GetCateGrIdeaByID(ID);
            return View(result);
        }
        [HttpPost]
        public ActionResult Update(CategoryGroupIdea model) { 
            model.ModifiedBy = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).Username;
            var result = new CateGrIdeaDao().UpdateGrCateIdea(model);
          
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int ID)
        {
            var result = new CateGrIdeaDao().DeleteCateGrIdea(ID);

            return RedirectToAction("Index");
        }
    }
}