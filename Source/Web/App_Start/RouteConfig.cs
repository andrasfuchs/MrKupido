using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MrKupido.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "AccountManagement",
                url: "{language}/Account/{action}",
                defaults: new { language = "hun", controller = "Account", action = "Profile" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "{language}/Admin/{controller}/{action}/{id}",
                defaults: new { language = "hun", controller = "Ingredient", action = "Index", id = UrlParameter.Optional,  }
            );

            routes.MapRoute(
                name: "Default",
                url: "{language}/{controller}/{id}/{action}",
                defaults: new { language="hun", controller = "Home", id = UrlParameter.Optional, action = "Index" }
            );        
        }
    }
}