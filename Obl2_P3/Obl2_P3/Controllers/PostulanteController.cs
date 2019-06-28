using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obl2_P3.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Obl2_P3.Controllers
{
    public class PostulanteController : Controller
    {

        #region Logic

        #region GET

        // GET: Postulante/CrearPostulante
        public ActionResult CrearPostulante()
        {
            return View();
        }


        #endregion

        #region POST
        //No funciona

        // POST: Postulante/CrearPostulante
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPostulante(VMPostulante postulante)
        {
            if (ModelState.IsValid)
            {
                HttpClient cliente = new HttpClient();
                Uri crearPostulante = new Uri("http://localhost:50310/api/RegisterPostulante");

                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new HttpResponseMessage();


                var post = cliente.PostAsJsonAsync(crearPostulante, postulante);

                var result = post.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["ResultadoOperacion"] = "Postulante ingresado con éxito";
                    return RedirectToAction("CrearPostulante", new VMPostulante());
                }
                return View(postulante);
            }
            else
            {
                TempData["ResultadoOperacion"] = "Errores de ingreso";

                return RedirectToAction("CrearPostulante", new VMPostulante());
            }
        }

        #endregion

        #endregion

    }
}