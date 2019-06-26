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
<<<<<<< HEAD
                defaults: new { controller = "Vivienda", action = "GetByManyBedrooms" }
=======
                defaults: new
                {
                    controller = "Vivienda",
                    action = "GetByManyBedrooms"
                }
>>>>>>> 38bb6025fee3845f39aef87694c70a68ec77f800
            );


            config.Routes.MapHttpRoute(
                name: "GetByPriceRange",
                routeTemplate: "api/GetByPriceRange/{pMin:decimal}/{pMax:decimal}",
                defaults: new
                {
                    controller = "Vivienda",
                    action = "GetByPriceRange",
<<<<<<< HEAD
                    pMin = RouteParameter.Optional,
=======
>>>>>>> 38bb6025fee3845f39aef87694c70a68ec77f800
                    pMax = RouteParameter.Optional
                });


            config.Routes.MapHttpRoute(
               name: "GetByBarrio",
<<<<<<< HEAD
               routeTemplate: "api/GetByBarrio/{idBarrio}",
=======
               routeTemplate: "api/GetByBarrio/{idBarrio:int}",
>>>>>>> 38bb6025fee3845f39aef87694c70a68ec77f800
               defaults: new
               {
                   controller = "Vivienda",
                   action = "GetByBarrio"
<<<<<<< HEAD
               }
           );
=======
               });

>>>>>>> 38bb6025fee3845f39aef87694c70a68ec77f800

            config.Routes.MapHttpRoute(
               name: "GetByState",
               routeTemplate: "api/GetByState/{state:int}",
<<<<<<< HEAD
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

=======
               defaults: new
               {
                   controller = "Vivienda",
                   action = "GetByState"
               });


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
>>>>>>> 38bb6025fee3845f39aef87694c70a68ec77f800
        }
    }
}
