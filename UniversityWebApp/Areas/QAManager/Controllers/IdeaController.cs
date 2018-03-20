using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityDao.Dao;
using UniversityDao.EF;

namespace UniversityWebApp.Areas.QAManager.Controllers
{
    public class IdeaController : Controller
    {
        // GET: QAManager/Idea
        public ActionResult Index(int idCateId)
        {
            var listIdea = new IdeaDao().GetAllIdeaByCateId(idCateId);
            Session["ideaCateId"] = idCateId;
            return View(listIdea);
        }

        public JsonResult GetCateIdeaByJs(int grCateId)
        {
            var result = new IdeaCategoryDao().GetIdeaCateByGr(grCateId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public void GetNameIdeaCate(int ideaCateId)
        {
            ViewBag.IdeaCateName = new IdeaDao().GetNameIdeaCate(ideaCateId);
        }

        public bool RemoveIdeaById(int ideaId)
        {
            new IdeaDao().RemoveIdeaById(ideaId);
            return true;
        }
        [HttpGet]
        public ActionResult UpdateIdeaById(int ideaId)
        {
            var cateGrId = TempData["grID"];
            ViewBag.GroupCateIdea = new SelectList(new CateGrIdeaDao().GetAllGrCateIdea(), "CategoryGroupIdeaID", "CategoryGroupName", cateGrId);
            Idea idea = new IdeaDao().GetIdeaById(ideaId);
            return View(idea);
        }
        [HttpPost]
        public ActionResult UpdateIdeaById(Idea idea, HttpPostedFileBase file)
        {
            idea.ModifiedBy = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).Username;
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string pathToSave = Server.MapPath("~/Data/StudentData/");
                string path = Path.Combine(pathToSave, fileName);
                file.SaveAs(path);
                idea.FileSP = "~/Data/StudentData/" + fileName;
            }
            new IdeaDao().UpdateIdeaById(idea);
            return RedirectToAction("Index/",new { idCateId = idea.IdeaCategory });
        }
    }
}