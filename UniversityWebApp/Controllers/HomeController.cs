
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityDao.Dao;
using UniversityDao.EF;
using UniversityWebApp.Models;

namespace UniversityWebApp.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            List<CategoryIdeaView> lvIdeaCateView = new List<CategoryIdeaView>();
            List<CategoryGroupIdea> listCateGroupIdea = new List<CategoryGroupIdea>();
            listCateGroupIdea = new CateGrIdeaDao().GetAllGrCIBySt();
            
            foreach (var item in listCateGroupIdea)
            {
                CategoryIdeaView categoryIdeaView = new CategoryIdeaView();
                categoryIdeaView.CateGr = item;

                List<IdeaCategoryModelView> lvIdeaCate = new IdeaCategoryDao()
                    .GetAllCateIDByGrId(item.CategoryGroupIdeaID)
                    .Select(x => new IdeaCategoryModelView
                    {
                        CategoryDescription = x.CategoryDescription,
                        IdeaCategoryID = x.IdeaCategoryID,
                        CategoryName = x.CategoryName,
                        LatestPost = (new IdeaDao().GetLastestPost(x.IdeaCategoryID).IdeaTitle).ToString(),
                        IdLatestPost = new IdeaDao().GetLastestPost(x.IdeaCategoryID).IdeaID,
                        CountIdea = (new IdeaDao().GetAllIdeaByCateIdSt(x.IdeaCategoryID).Count())
                    }).ToList();

                categoryIdeaView.lvIdeaCate = lvIdeaCate;
                lvIdeaCateView.Add(categoryIdeaView);
            }

            ViewBag.CateGr = lvIdeaCateView;
            return View();
        }

    }
}