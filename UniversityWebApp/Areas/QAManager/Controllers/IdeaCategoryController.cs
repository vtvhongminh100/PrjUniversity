using Ionic.Zip;
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
    public class IdeaCategoryController : BaseController
    {
        // GET: QAManager/CategoryIdea
       
        public ActionResult Index(int ID)
        {
            var result = new IdeaCategoryDao().GetAllIdeaCategory(ID);
            TempData["grID"] = ID;
            return View(result);
        }
 
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(IdeaCategory model)
        {
            model.GroupCateIdea = (int)TempData["grID"];
            model.CreatedBy =((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).Username;
            var result = new IdeaCategoryDao().InsertIdeaCategory(model);
            return RedirectToAction("Index/" + (int)TempData["grID"]);
        }
        [HttpGet]
        public ActionResult Update(int id, int grID)
        {
            ViewBag.GroupCateIdea = new SelectList(new CateGrIdeaDao().GetAllGrCateIdea(), "CategoryGroupIdeaID", "CategoryGroupName", grID);
            var result = new IdeaCategoryDao().GetIdeaCateByID(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Update(IdeaCategory model)
        {
            model.ModifiedBy = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]).Username;
            var result = new IdeaCategoryDao().UpdateIdeaCategory(model);
            return RedirectToAction("Index/" + (int)TempData["grID"]);
        }
        public ActionResult Delete(int id)
        {
            new IdeaCategoryDao().DeleteIdeaCategory(id);
            return RedirectToAction("Index/" + (int)TempData["grID"]);
        }
        public ActionResult DownloadZipFile(int ideaCateId)
        {
            if (new IdeaCategoryDao().ChkFinaGtThanNow(ideaCateId/777) == false )
            {
                List<Idea> files = new IdeaDao().GetLFSPOverDateByCateId(ideaCateId/777);
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                //string fileName = path.Substring(path.LastIndexOf('/') + 1);
                if (files.Count > 0)
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        zip.AddDirectoryByName("Files");
                        foreach (var item in files)
                        {
                            zip.AddFile(basePath + item.FileSP.Replace("~", ""), "Files");
                        }
                        string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MM-dd"));
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            zip.Save(memoryStream);
                            return File(memoryStream.ToArray(), "application/zip", zipName);
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index/" + (int)TempData["grID"]);
                }
              
            }
            else
            {
                return null;
            }
         
        }

        //public ActionResult DownloadZipFile(int ideaId)
        //{
        //    if (new IdeaCategoryDao().CheckFinalCloseDate())
        //    {

        //    }
        //    string path = new IdeaDao().GetFileSP(ideaId / 777);
        //    List<string> files = new List<string>();
        //    string basePath = AppDomain.CurrentDomain.BaseDirectory + "Data/StudentData/";
        //    string fileName = path.Substring(path.LastIndexOf('/') + 1);


        //    files.Add(basePath + fileName);
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
        //        zip.AddDirectoryByName("Files");
        //        foreach (var item in files)
        //        {
        //            zip.AddFile(item, "Files");
        //        }
        //        string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MM-dd"));
        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            zip.Save(memoryStream);
        //            return File(memoryStream.ToArray(), "application/zip", zipName);
        //        }
        //    }
        //}
    }
}