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
        HttpClient client = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        Uri registerUri = new Uri("http://localhost:50310/api/RegisterPostulante");

        public PostulanteController()
        {
            client.BaseAddress = new Uri("http://localhost:50310");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        #region Logic

        #region GET

        // GET: Postulante/CrearPostulante
        public ActionResult CrearPostulante()
        {
            return View(new VMPostulante());
        }


        #endregion

        #region POST
        //404 NOT FOUND
        // POST: Postulante/RegisterPostulante
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPostulante(VMPostulante postulante)
        {
            if (ModelState.IsValid)
            {
                //var serializer = new JavaScriptSerializer();
                //var json = serializer.Serialize(postulante);
                //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                //var doPost = client.PostAsync(registerUri, stringContent);

                var doPost = client.PostAsJsonAsync(registerUri, postulante);

                var result = doPost.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["ResultadoOperacion"] = new string[] { "alert-success", "Postulante creado correctamente." };
                    return RedirectToAction("Index");
                }
                TempData["ResultadoOperacion"] = new string[] { "alert-danger", "Algo ha fallado." };
                return View(postulante);
            }
            else
            {
                TempData["ResultadoOperacion"] = new string[] { "", "" };

                return RedirectToAction("Index");
            }

        }

        #endregion

        #endregion

    }
}