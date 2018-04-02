using ModelPr.CommonClass;
using ModelPr.ModelViews;
using System.Net;
using System.Web.Mvc;
using UniversityDao.Dao;

namespace UniversityWebApp.Areas.QAManager.Controllers
{
    public class ProfileController : Controller
    {
        // GET: QAManager/Profile
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonCls.User_session];
            if (session == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDao dao = new AccountDao();
            var user = dao.GetUserByUserName(session.Username);
            ProfileModel profile = new ProfileModel()
            {
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.Phone,
                Image = user.Image
            };
            return View("Index", profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Password,ConfirmPassword,FullName,Address,Email,Phone")] ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonCls.User_session];
                if (session == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AccountDao dao = new AccountDao();
                var account = dao.GetUserByUserName(session.Username);
                account.FullName = profile.FullName;
                account.Address = profile.Address;
                account.Email = profile.Email;
                account.Phone = profile.Phone;
                if (profile.Password != null)
                {
                    account.Password = profile.Password;
                    dao.UpdatePassword(account);
                    // Destroy session

                    //
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    dao.UpdatePassword(account);
                    TempData["Message"] = "Update Successfully!";
                    return RedirectToAction("MyProfile", "Account");
                }
            }
            return View("Index", profile);
        }
    }
}