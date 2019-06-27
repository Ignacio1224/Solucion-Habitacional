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
            // Configuración del ruteo de la API (RCP (NO REST) )

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GetByManyBedrooms",
                routeTemplate: "api/GetByManyBedrooms/{cantDormitorios:int}",
                defaults: new
                {
                    controller = "Vivienda",
                    action = "GetByManyBedrooms"
                }

            );


            config.Routes.MapHttpRoute(
                name: "GetByPriceRange",
                routeTemplate: "api/GetByPriceRange/{pMin:decimal}/{pMax:decimal}",
                defaults: new
                {
                    controller = "Vivienda",
                    action = "GetByPriceRange",

                    pMin = RouteParameter.Optional,
                    pMax = RouteParameter.Optional
                });


            config.Routes.MapHttpRoute(
               name: "GetByBarrio",

               routeTemplate: "api/GetByBarrio/{idBarrio}",
               defaults: new
               {
                   controller = "Vivienda",
                   action = "GetByBarrio"

               }
           );

            config.Routes.MapHttpRoute(
               name: "GetByState",
               routeTemplate: "api/GetByState/{state:int}",
               defaults: new
               {
                   controller = "Vivienda",
                   action = "GetByState"
               }
           );

            config.Routes.MapHttpRoute(
                name: "GetByType",
                routeTemplate: "api/GetByType/{type:alpha}",
                defaults: new
                {
                    controller = "Vivienda",
                    action = "GetByType"
                }
            );

            config.Routes.MapHttpRoute(
                name: "RegisterPostulante",
                routeTemplate: "api/RegisterPostulante/{p}",
                defaults: new
                {
                    controller = "Vivienda",
                    action = "RegisterPostulante"
                }
            );

            config.Routes.MapHttpRoute(
               name: "GetByType",
               routeTemplate: "api/GetByType/{type:alpha}",
               defaults: new
               {
                   controller = "Vivienda",
                   action = "GetByType"
               });

            config.Routes.MapHttpRoute(
             name: "RegisterPostulante",
             routeTemplate: "api/RegisterPostulante/{p}",
             defaults: new
             {
                 controller = "Postulante",
                 action = "RegisterPostulante"
             });

        }
    }
}
