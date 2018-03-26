using System.Web.Mvc;

namespace UniversityWebApp.Areas.QACoordinator
{
    public class QACoordinatorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QACoordinator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QACoordinator_default",
                "QACoordinator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}