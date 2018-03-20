using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UniversityWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //    routes.MapRoute(
            //     "Dowdload",
            //    "Idea/a/",
            //    new { controller = "Home", action = "Index" },
            //     new[] { "UniversityWebApp.Controllers" }
            //);
            routes.MapRoute(
               "Dowdload",
              "dl/{id}",
              new { controller = "Idea", action = "DownloadFile", id = UrlParameter.Optional },
               new[] { "UniversityWebApp.Areas.Student.Controllers" }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}
