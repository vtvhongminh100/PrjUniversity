using Ionic.Zip;
using Model.ModelViews;
using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using UniversityDao.Dao;
using UniversityDao.EF;
using UniversityWebApp.Models;

namespace UniversityWebApp.Areas.Student.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]

    //[SessionState(SessionStateBehavior.Disabled)]
    public class IdeaController : BaseController
    {
        // GET: Student/Idea
        public ActionResult Index(int ideaCateId)
        {
            var result = new IdeaDao().GetAllIdeaByCateIdSt(ideaCateId).Select(x => new IdeaView
            {
                IdeaID = x.IdeaID,
                IdeaTitle = x.IdeaTitle,
                IdeaViewCount = x.IdeaViewCount,
                CommentCount = (int)GetCountComment(x.IdeaID)
            });
            
            GetNameIdeaCate(ideaCateId);
            return View(result);
        }
        
        public int GetCountComment(int ideaId)
        {
            var vc = new CommentDao().GetCmByIdeaId(ideaId).Count;
            return vc;
        }
        // Add View count to idea table
        public void InsertViewCount(int ideaId)
        {
            new IdeaDao().InsertViewCount(ideaId);
        }
        public void GetCmByIdeaId(int ideaId)
        {
            var rs = new CommentDao().GetCmByIdeaId(ideaId);
            ViewBag.ViewAllComment = rs;
        }
        public void GetUserByideaId(int ideaId)
        {
            var rs = new AccountDao().GetUserByIdeaId(ideaId);
            ViewBag.ShowUserPostIdea = rs;
            GetCmByIdeaId(ideaId);
        }
        [HttpGet]
        public ActionResult PostIdea()
        {
            ViewBag.GroupCateIdea = new SelectList(new CateGrIdeaDao().GetAllGrCateIdea(), "CategoryGroupIdeaID", "CategoryGroupName");
            return View();
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
        public void ThumbsUp(int ideaId)
        {

            var userId = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).UserID;
            new IdeaDao().CheckExistTD(ideaId, userId);
            var rs_exEmotion = new IdeaDao().CheckExistTU(ideaId, userId);

            if (rs_exEmotion == 0)
            {
                new IdeaDao().ThumbsUp(ideaId, userId);
            }
        }

        public JsonResult GetThumbsUpCountUC(int ideaId)
        {
            int ThumbsCount = new IdeaDao().GetThumbsUp(ideaId).Count();
            return Json(ThumbsCount, JsonRequestBehavior.AllowGet);
        }

        public void ThumbsDown(int ideaId)
        {

            var userId = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).UserID;
            new IdeaDao().CheckExistTU(ideaId, userId);
            var rs_exEmotion = new IdeaDao().CheckExistTD(ideaId, userId);
            if (rs_exEmotion == 0)
            {
                new IdeaDao().ThumbsDown(ideaId, userId);
            }
        }
        public JsonResult CheckExistTU(int ideaId)
        {
            var userId = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).UserID;
            var rs_exEmotion = new IdeaDao().GetExistTU(ideaId, userId);
            return Json(rs_exEmotion, JsonRequestBehavior.AllowGet);

        }
        public JsonResult CheckExistTD(int ideaId)
        {
            var userId = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).UserID;
            var rs_exEmotion = new IdeaDao().GetExistTD(ideaId, userId);
            return Json(rs_exEmotion, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetThumbsDownCountUC(int ideaId)
        {
            int ThumbsCount = new IdeaDao().GetThumbsDown(ideaId).Count();
            return Json(ThumbsCount, JsonRequestBehavior.AllowGet);
        }
        // STUDENT 
        [HttpGet]
        public ActionResult GetDtIdById(int id)
        {
            InsertViewCount(id);
            GetUserByideaId(id);
            string path = new IdeaDao().GetFileSP(id);
            if (path != null)
            {
                string fileName = path.Substring(path.LastIndexOf('/') + 1);
                ViewBag.FileNameSP = fileName;
            }
            ViewBag.ViewIdea = new IdeaDao().GetIdeaByIdSt(id);
            return View();
        }
        [HttpGet]
        public FileContentResult DownloadFile(int ideaId)
        {
          
            string path = new IdeaDao().GetFileSP(ideaId / 777);
            string fileName = path.Substring(path.LastIndexOf('/') + 1);
            if (((UserLogin)(Session[ModelPr.CommonClass.CommonCls.User_session])).UserID != 0)
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory + "Data/StudentData/";
                byte[] fileBytes = System.IO.File.ReadAllBytes(basePath + fileName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                return null;
            }
        }
      
        [HttpPost]
        public ActionResult PostIdea(Idea model, HttpPostedFileBase file)
        {


            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string pathToSave = Server.MapPath("~/Data/StudentData/");
                string path = Path.Combine(pathToSave, fileName);
                file.SaveAs(path);
                model.FileSP = "~/Data/StudentData/" + fileName;

            }
            model.CreatedBy = ((UserLogin)(Session[ModelPr.CommonClass.CommonCls.User_session])).Username;
            new IdeaDao().InsertNewIdea(model);

            return RedirectToAction("PostIdea");
        }
    }
}