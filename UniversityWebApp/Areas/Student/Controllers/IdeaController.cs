using Ionic.Zip;
using Model.ModelViews;
using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                CommentCount = (int)GetCountComment(x.IdeaID),
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy
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

        public void GetUserByideaId(int ideaId)
        {
            var rs = new AccountDao().GetUserByIdeaId(ideaId);
            ViewBag.ShowUserPostIdea = rs;
            CheckShowCmByRole(ideaId);
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
            ViewBag.IdeaCateName = new IdeaDao().GetIdeaCateById(ideaCateId).CategoryName.ToString();
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
        // MINH / ADD  NEW IDEA / STU
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
            int ideaId = new IdeaDao().InsertNewIdea(model);
            string url = Url.Action("UpdateIdeaById", "Idea", new { area = "Admin", ideaId = ideaId }, Request.Url.Scheme);
            string linkCheck = "<a href=" + url + ">" + url + "</a>";

            if (ideaId != 0)
            {
                var emailRc = (ConfigurationManager.AppSettings["ToEmailDisplayName"]);
                MailMessage m = new MailMessage(
                             new MailAddress(ConfigurationManager.AppSettings["FromEmailAddress"]),
                             new MailAddress(ConfigurationManager.AppSettings["ToEmailAddress"]));
                m.Subject = "New idea posted";
                m.Body = string.Format("Dear {0}<BR/>New idea posted, please click on the below link to:<BR/> <a href=\"{1}\" title=\"User Email Confirm\">See</a>", emailRc, Url.Action("UpdateIdeaById", "Idea", new { Area = "QAManager", ideaId = ideaId }, Request.Url.Scheme));

                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["mailAccount"],
                    ConfigurationManager.AppSettings["mailPassword"]
                    );
                smtp.EnableSsl = true;
                smtp.Send(m);
            };

            return RedirectToAction("PostIdea");
        }
        // MINH / SHOW COMMENT DEFENT ON ROLE FOT STUDENT/ STUDENT
        public void GetCommentForStu(int ideaId)
        {
            var rs = new CommentDao().GetCmByIdeaIdForStu(ideaId);
            ViewBag.ViewAllComment = rs;
        }
        // MINH / SHOW ALL COMMENT / STUDENT
        public void GetCmByIdeaId(int ideaId)
        {
            var rs = new CommentDao().GetCmByIdeaId(ideaId);
            ViewBag.ViewAllComment = rs;
        }
        // MINH / CHECK SHOW COMMENT/ STUDENT
        public void CheckShowCmByRole(int ideaId)
        {

            Account roleUser = (new AccountDao().GetUserByID(((UserLogin)(Session[ModelPr.CommonClass.CommonCls.User_session])).UserID));
            if (roleUser.Role.Equals("STU"))
            {
                GetCommentForStu(ideaId);
            }
            else
            {
                GetCmByIdeaId(ideaId);
            }
        }

    }
}