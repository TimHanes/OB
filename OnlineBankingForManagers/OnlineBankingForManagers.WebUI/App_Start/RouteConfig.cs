using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineBankingForManagers.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Client",
                    action = "List",
                    status = (string)null,
                    page = 1
                });


            routes.MapRoute(null,
                 "Page{page}",
                 new
                 {
                     controller = "Client",
                     action = "List",
                     status = (string)null},
                     new { page = @"\d+" }
                 );
             routes.MapRoute(null,
        "{status}",
        new { controller = "Client", action = "List", page = 1 }
      );

      routes.MapRoute(null,
        "{status}/Page{page}",
        new { controller = "Client", action = "List" },
        new { page = @"\d+" }
      );

      routes.MapRoute(null, "{controller}/{action}");
        }
    }
}