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
using UniversityDao.EF;
using System.Net.Mail;
using System.Configuration;

namespace UniversityWebApp.Controllers {
    public class AccountController : Controller {
        // GET: Login

        public AccountController() {

        }
        public ActionResult Index() {
            return View();
        }
        public ActionResult Logout() {
            Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model) {
            Session.Clear();
            if (ModelState.IsValid) {
                AccountDao dao = new AccountDao();
                int x = dao.LoginCheck(model);
                if (x == 0) {
                    ModelState.AddModelError("", "This account is not exist !!!");
                } else if (x == 2) {
                    ModelState.AddModelError("", "The account is locked !!!");
                } else if (x == 1) {
                    var user = dao.GetUserByUserName(model.Username);
                    var usersession = new UserLogin();
                    usersession.Username = user.Username;
                    usersession.UserID = user.UserID;
                    Session.Add(CommonCls.User_session, usersession);
                    return RedirectToAction("Index", "Home", new { area = "" });
                } else if (x == -1) {
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
        public ActionResult Registry() {
            return View("Registry");
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(AccountModel model) {
            if (ModelState.IsValid) {
                AccountDao dao = new AccountDao();
                if (dao.GetUserByEmail(model.Email) != null) {
                    ModelState.AddModelError("", "This email is existing !!!");
                } else {
                    string username = "Student" + dao.GetNextStudentID().ToString("D4");
                    string password = System.Web.Security.Membership.GeneratePassword(12, 1);
                    model.Username = username;
                    model.Password = password;
                    Account acc = dao.Registry(model);
                    if (acc != null) {
                        string html = System.IO.File.ReadAllText(Path.Combine(HttpRuntime.AppDomainAppPath, "mail.html"));
                        MailMessage m = new MailMessage(
                        new MailAddress(ConfigurationManager.AppSettings["SMTPUserName"], "FPT Greenwich University Forum"),
                        new MailAddress(model.Email));
                        m.Subject = "Email confirmation";
                        m.Body = string.Format(html, model.FullName, model.Username, model.Password, Url.Action("ConfirmEmail", "Account", new { Token = acc.Token }, Request.Url.Scheme));
                        m.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTPHost"], Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]));
                        smtp.Credentials = new NetworkCredential(
                            ConfigurationManager.AppSettings["SMTPUserName"],
                            ConfigurationManager.AppSettings["SMTPPassword"]
                            );
                        smtp.EnableSsl = true;
                        smtp.Send(m);
                        return RedirectToAction("Confirm", "Account", new { Email = model.Email });
                    } else {
                        ModelState.AddModelError("", "The account did not create");
                    }
                }
            }
            return View("Registry");
        }
        [AllowAnonymous]
        public ActionResult Confirm(string Email) {
            ViewBag.Email = Email;
            return View();
        }
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string Token) {
            AccountDao dao = new AccountDao();
            bool x = dao.UpdateEmailConfirmed(Token);
            if (x == true) {
                return View("Thanks");
            } else {
                return View("Error");
            }
        }
        public ActionResult ResetPass() {
            return View("ResetPassword");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPass(ResetPassword model) {
            if (ModelState.IsValid) {
                if (model.Email == null) {
                    ModelState.AddModelError("", "The Email field is required.");
                } else {
                    AccountDao dao = new AccountDao();
                    Account account = dao.GetUserByEmail(model.Email);

                    if (account == null) {
                        ModelState.AddModelError("", "This email is not existing !!!");
                    } else {
                        MailMessage m = new MailMessage(
                        new MailAddress(ConfigurationManager.AppSettings["mailAccount"], "FPT Greenwich University Forum"),
                        new MailAddress(model.Email));
                        m.Subject = "Reset Password";
                        m.Body = string.Format("Dear {0}<BR/>Please reset your password by clicking here: <a href=\"{1}\" title=\"Reset Password\">link</a>", account.Username, Url.Action("ChangePassword", "Account", new { Token = account.Token }, Request.Url.Scheme));
                        m.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Credentials = new NetworkCredential(
                            ConfigurationManager.AppSettings["mailAccount"],
                            ConfigurationManager.AppSettings["mailPassword"]
                            );
                        smtp.EnableSsl = true;
                        smtp.Send(m);
                        return RedirectToAction("Confirm", "Account", new { Email = model.Email });
                    }
                }
            }
            return View("ResetPassword");
        }
        public ActionResult ChangePassword(string Token) {
            AccountDao dao = new AccountDao();
            Account account = dao.GetUserByToken(Token);
            ResetPassword rp = new ResetPassword();
            rp.Email = account.Email;
            return View("ChangePassword", rp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ResetPassword model) {
            if (ModelState.IsValid) {
                if (model.Password == null) {
                    ModelState.AddModelError("", "The Password field is required.");
                } else if (model.ConfirmPassword == null) {
                    ModelState.AddModelError("", "The Confirm Password field is required.");
                } else {
                    AccountDao dao = new AccountDao();
                    bool x = dao.ResetPassword(model);
                    if (x == true) {
                        return View("Thanks");
                    } else {
                        return View("Error");
                    }
                }
            }
            return View("ChangePassword");
        }

    }
}