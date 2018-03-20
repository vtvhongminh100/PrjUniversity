using ModelPr.CommonClass;
using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UniversityDao.Dao;

namespace UniversityWebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login

        public AccountController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountModel model)
        {
            Session.Clear();
            if (ModelState.IsValid)
            {
                AccountDao dao = new AccountDao();
                int x = dao.LoginCheck(model);
                if (x == 0)
                {
                    ModelState.AddModelError("", "This account is not exist !!!");
                }
                else if (x == 2)
                {
                    ModelState.AddModelError("", "The account is locked !!!");
                }
                else if (x == 1)
                {
                    var user = dao.GetUserByUserName(model.Username);
                    var usersession = new UserLogin();
                    usersession.Username = user.Username;
                    usersession.UserID = user.UserID;
                    Session.Add(CommonCls.User_session, usersession);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else if (x == -1)
                {
                    ModelState.AddModelError("", "Password is not correct !!!");
                }
            }
            return View("Index");
        }
    }
}