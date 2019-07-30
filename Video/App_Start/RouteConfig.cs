using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Video.Areas.API;

namespace Video
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                constraints: new { controller = "^(?!Service).*" },
                namespaces: new[] { "video.Controllers" }
            );
            routes.Add(new ServiceRoute("Service", new WebServiceHostFactory(), typeof(VideoService)));
        }
    }
}
