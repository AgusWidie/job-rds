using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using WEB_API_DASHBOARD.Services;

namespace WEB_API_DASHBOARD
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            // Web API configuration and services

            // List of delegating handlers.
            DelegatingHandler[] handlers = new DelegatingHandler[] {
                new TokenValidation()
            };

            // Create a message handler chain with an end-point.
            var routeHandlers = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config), handlers);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "authentication",
                routeTemplate: "auth/login",
                defaults: new { controller = "Auth", action = "Post" }
            );

            //config.Routes.MapHttpRoute(
            //    name: "authenticationMobile",
            //    routeTemplate: "mobile/login",
            //    defaults: new { controller = "Auth", action = "MobilePost" }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Svc/{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional },
                constraints: null,
                handler: routeHandlers
            );

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
            new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

        }
    }
}
