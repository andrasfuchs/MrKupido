using MrKupido.Web.Models;
using System.Web.Http;
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
                defaults: new { language = "xxx", controller = "Home", action = "Index", id = System.Web.Mvc.UrlParameter.Optional }
            );

        }
    }
}