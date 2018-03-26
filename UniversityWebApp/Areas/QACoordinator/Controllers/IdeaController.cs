using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityDao.Dao;

namespace UniversityWebApp.Areas.QACoordinator.Controllers
{
    public class IdeaController : BaseController
    {
        // GET: QACoordinator/Idea
        public ActionResult Index()
        {
            return View();
        }
        public IdeaController()
        {
            IdeaReport();
        }
        public JsonResult IdeaReport()
        {
            var rs = new IdeaDao().GetCountReport(DateTime.Now);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}