using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Young.Web.Hotel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Hotels", action = "Index", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
            name: "RestSearchAPI",
            url: "api/{action}/{id}",
            defaults: new { controller = "Hotels", id = UrlParameter.Optional }
            );           

        }
    }
}
