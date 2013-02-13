using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace MrKupido.Web.Models
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
            routes.Add(name, newRoute);
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
            var newRoute = new NamedRoute(name, url, new MvcRouteHandler());
            newRoute.Defaults = new RouteValueDictionary(defaults);
            newRoute.Constraints = new RouteValueDictionary(constraints);
            routes.Add(name, newRoute);
        }
    }
}