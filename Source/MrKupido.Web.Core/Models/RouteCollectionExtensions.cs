using Microsoft.AspNetCore.Routing;

namespace MrKupido.Web.Core.Models
{
    public static class RouteCollectionExtensions
    {
        public static void IgnoreRoute(this RouteCollection routes, string url)
        {
            routes.IgnoreRoute(string.Empty, url, null);
        }

        public static void IgnoreRoute(this RouteCollection routes, string name, string url)
        {
            routes.IgnoreRoute(name, url, null);
        }

        public static void IgnoreRoute(this RouteCollection routes, string name, string url, object constraints)
        {
            var newRoute = new NamedRoute(name, url, new StopRoutingHandler());
            routes.Add(newRoute);
        }

        public static void MapRoute(this RouteCollection routes, string name, string url)
        {
            routes.MapRoute(name, url, null, null);
        }

        public static void MapRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapRoute(name, url, defaults, null);
        }

        public static void MapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            var newRoute = new NamedRoute(name, url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler());
            routes.Add(newRoute);
        }
    }
}