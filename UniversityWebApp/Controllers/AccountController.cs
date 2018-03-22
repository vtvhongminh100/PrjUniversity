using ModelPr.CommonClass;
using ModelPr.ModelViews;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
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

        public ActionResult MyProfile() {
            var session = (UserLogin)Session[CommonCls.User_session];
            if (session == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountDao dao = new AccountDao();
            var user = dao.GetUserByUserName(session.Username);
            ProfileModel profile = new ProfileModel() {
                Address = user.Address, Email = user.Email, FullName = user.FullName, Phone = user.Phone, Image = user.Image
            };
            return View("Profile", profile);
        }

        [HttpPost]
        public ActionResult UploadAvatar(HttpPostedFileBase file) {
            try {
                var guid = Guid.NewGuid().ToString();
                string today = DateTime.Now.ToShortDateString().Replace("/", "");
                string extension = Path.GetExtension(file.FileName);
                string fName = today + "-" + guid + extension;
                var allowedExtensions = new[] { ".png", ".jpg", ".gif" };
                if (allowedExtensions.Contains(extension.ToLower()) && file.ContentLength <= (1000 * 1024) && file.ContentLength > 0) {

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\", Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Uploaded");

                    var fileName1 = Path.GetFileName(fName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, fName);
                    file.SaveAs(path);
                    //
                    var session = (UserLogin)Session[CommonCls.User_session];
                    if (session == null) {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    AccountDao dao = new AccountDao();
                    dao.UploadImage(session.UserID, fName);
                    //
                    TempData["Message"] = "Upload Successfully!";
                    return RedirectToAction("MyProfile", "Account");
                } else {
                    TempData["Message"] = "Please upload image file within 1 MB!";
                    return RedirectToAction("MyProfile", "Account");
                }
            } catch (Exception ex) {

            }
            return RedirectToAction("MyProfile", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile([Bind(Include = "Password,ConfirmPassword,FullName,Address,Email,Phone")] ProfileModel profile) {
            if (ModelState.IsValid) {
                var session = (UserLogin)Session[CommonCls.User_session];
                if (session == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AccountDao dao = new AccountDao();
                var account = dao.GetUserByUserName(session.Username);
                account.FullName = profile.FullName;
                account.Address = profile.Address;
                account.Email = profile.Email;
                account.Phone = profile.Phone;
                if (profile.Password != null) {
                    account.Password = profile.Password;
                    dao.UpdatePassword(account);
                    // Destroy session

                    //
                    return RedirectToAction("Index", "Account");
                } else {
                    dao.UpdatePassword(account);
                    TempData["Message"] = "Update Successfully!";
                    return RedirectToAction("MyProfile", "Account");
                }
            }
            return View("Profile", profile);
        }
    }
}