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

            //Configuración de ruteos para controller Vivienda

            config.Routes.MapHttpRoute(
                name: "GetByManyBedrooms",
                routeTemplate: "api/GetByManyBedrooms/{cantDormitorios}",
                defaults: new
                {
                    controller = "Vivienda",
                    action = "GetByManyBedrooms"
                }

            );

            config.Routes.MapHttpRoute(
                name: "GetByPriceRange",
                routeTemplate: "api/GetByPriceRange/{pMin}/{pMax}",
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
               routeTemplate: "api/GetByState/{state}",
               defaults: new
               {
                   controller = "Vivienda",
                   action = "GetByState"
               }
           );

            config.Routes.MapHttpRoute(
                name: "GetByType",
                routeTemplate: "api/GetByType/{type}",
                defaults: new
                {
                    controller = "Vivienda",
                    action = "GetByType"
                }
            );

            //Configuración ruteo para el controller de postulante
            config.Routes.MapHttpRoute(
             name: "RegisterPostulante",
             routeTemplate: "api/RegisterPostulante",
             defaults: new
             {
                 controller = "Postulante",
                 action = "RegisterPostulante"
             });

        }
    }
}
