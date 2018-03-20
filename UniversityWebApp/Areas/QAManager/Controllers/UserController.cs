using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityDao.Dao;
using UniversityDao.EF;

namespace UniversityWebApp.Areas.QAManager.Controllers
{
    public class UserController : BaseController
    {
        //User controller is for admin's usage
        //Purpose: Managing User infos

        // GET: QAManager/User
        public ActionResult Index()
        {
            var result = new AccountDao().GetAllAccount();
            return View(result);
        }

        // GET: QAManager/User/Details/5
        public ActionResult Details(int id)
        {
            var result = new AccountDao().GetUserByID(id);
            return View();
        }

        // GET: QAManager/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QAManager/User/Create
        [HttpPost]
        public ActionResult Create(Account model)
        {
            try
            {
                new AccountDao().CreateAccount(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QAManager/User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QAManager/User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Account model)
        {
            try
            {
                new AccountDao().EditAccount(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QAManager/User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QAManager/User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Account model)
        {
            try
            {
                new AccountDao().EditAccount(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
