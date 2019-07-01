using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Repositorios;
using Obl2_P3.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Obl2_P3.Controllers
{
    public class PostulanteController : Controller
    {
        #region ApiRegion


        HttpClient cliente = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        Uri productoUri = null;

        public PostulanteController()
        {
            cliente.BaseAddress = new Uri("http://localhost:50265");
            productoUri = new Uri("http://localhost:50265/api/RegisterPostulante");
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        #region Logic

        #region GET

        // GET: Postulante/CrearPostulante
        public ActionResult CrearPostulante()
        {
            return View(new VMPostulante());
        }


        #endregion

        #region POST

        // POST: Postulante/RegisterPostulante
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPostulante(VMPostulante vmp)
        {
            #region ApiMethod
            //if (ModelState.IsValid)
            //{
            //    //var serializer = new JavaScriptSerializer();
            //    //var json = serializer.Serialize(postulante);
            //    //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            //    //var doPost = client.PostAsync(registerUri, stringContent);

            //    var tareaPost = cliente.PostAsJsonAsync(productoUri, VMPostulante.ConvertToVMPostulanteAPI(postulante));

            //    var result = tareaPost.Result;

            //    if (result.IsSuccessStatusCode)
            //    {
            //        TempData["ResultadoOperacion"] = new string[] { "alert-success", "Postulante creado correctamente." };
            //        return RedirectToAction("Index");
            //    }
            //    TempData["ResultadoOperacion"] = new string[] { "alert-danger", "Algo ha fallado." };
            //    return View(postulante);
            //}
            //else
            //{
            //    TempData["ResultadoOperacion"] = new string[] { "", "" };

            //    return RedirectToAction("Index");
            //}
            #endregion

            if (vmp.esValido())
            {
                RepoPostulante rp = new RepoPostulante();

                if (rp.add(VMPostulante.ConvertToPostulante(vmp)) )
                {
                    ViewBag.message = new string[] { "alert-success", "padding: 1em; margin-bottom: 0.6em;", "Postulante ingresado con éxito." };

                    return View(new VMPostulante());
                }
                else
                {
                    ViewBag.message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "El postulante no ha podido ser ingresado.<br>Verifica tus datos." };

                    return View(vmp);
                }

            }
            else
            {
                ViewBag.message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Ha ocurrido un error!" };

                return View(vmp);
            }
        }

        #endregion

        #endregion

    }
}