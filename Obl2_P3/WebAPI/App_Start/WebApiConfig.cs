using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GetByManyBedrooms",
                routeTemplate: "api/{controller}/{cantDormitorios}",
                defaults: new { cantDormitorios = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
                name: "GetByPriceRange",
                routeTemplate: "api/{controller}/{pMin}/{pMax}",
                defaults: new
                {
                    pMin = RouteParameter.Optional,
                    pMax = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
               name: "GetByBarrio",
               routeTemplate: "api/{controller}/{idBarrio}",
               defaults: new
               {
                   idBarrio = RouteParameter.Optional
               }
           );

            config.Routes.MapHttpRoute(
               name: "GetByState",
               routeTemplate: "api/{controller}/{state}",
               defaults: new
               {
                   state = RouteParameter.Optional
               }
           );

            config.Routes.MapHttpRoute(
           name: "GetByType",
           routeTemplate: "api/{controller}/{type}",
           defaults: new
           {
               type = RouteParameter.Optional
           }
       );
        }
    }
}
