
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
                        CountIdea = (new IdeaDao().GetAllIdeaByCateIdSt(x.IdeaCategoryID).Count()),
                        CreatedBy = x.CreatedBy
                    }).ToList();

                categoryIdeaView.lvIdeaCate = lvIdeaCate;
                lvIdeaCateView.Add(categoryIdeaView);
            }

            ViewBag.CateGr = lvIdeaCateView;
            return View();
        }
        public ActionResult GetNewPost()
        {
            List<Idea> lstIdea = new IdeaDao().GetNewPost();
            List<IdeaView> ideaViews = lstIdea.Select(x => new IdeaView
            {
                IdeaTitle = x.IdeaTitle,
                IdeaID = x.IdeaID,
                IdeaViewCount = x.IdeaViewCount,
                CommentCount = new CommentDao().GetCmByIdeaId(x.IdeaID).Count,
                CreatedDate = (DateTime)x.CreatedDate,
                CreatedBy = x.CreatedBy

            }).ToList();
            return View(ideaViews);
        }
        public ActionResult GetHelp()
        {
            return View();
        }
        public ActionResult HelpDetail()
        {
            return View();
        }
    }

}