using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityDao.EF;

namespace UniversityWebApp.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UniversityDbContext db = new UniversityDbContext();
            var session = ((UserLogin)Session[ModelPr.CommonClass.CommonCls.User_session]);
            if (session == null)
            {

            }
            else if (session != null)
            {
                var userRole = db.Accounts.Find(session.UserID).Role;
                if (userRole.Equals("QAM"))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "CateGrIdea", action = "Index", Area = "QAManager" }));
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}