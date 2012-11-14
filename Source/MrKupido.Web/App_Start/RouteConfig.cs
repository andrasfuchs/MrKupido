﻿using System;
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

            routes.MapRoute(
                name: "AccountManagement",
                url: "{language}/account/{action}",
                defaults: new { language = "hun", controller = "Account", action = "Profile" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "{language}/admin/{controller}/{action}/{id}",
                defaults: new { language = "hun", controller = "Ingredient", action = "Index", id = UrlParameter.Optional,  }
            );

            routes.MapRoute(
                name: "OldBrowser",
                url: "hun/home/notsupportedbrowser",
                defaults: new { language = "hun", controller = "Home", action = "NotSupportedBrowser" }
            );

            routes.MapRoute(
                name: "RecipeHun",
                url: "hun/recept/{id}",
                defaults: new { language = "hun", controller = "Recipe", action = "Details" }
            );

            routes.MapRoute(
                name: "RecipeEng",
                url: "eng/recipe/{id}",
                defaults: new { language = "eng", controller = "Recipe", action = "Details" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new { language = "hun", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );        

        }
    }
}