using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obl2_P3.Controllers
{
    public class ViviendaController : Controller
    {
        // GET: Vivienda
        public ActionResult Index()
        {
            ViewBag.message = new string[] { "d-none", "", "" };

            RepoVivienda rv = new RepoVivienda();

            return View(rv.findAll());
        }

        // GET: Vivienda/Details/5
        public ActionResult Details(int id)
        {
            RepoVivienda rv = new RepoVivienda();

            return View("Details", rv.findById(id));
        }

        // POST: Vivienda/Import
        [HttpPost]
        public ActionResult Import()
        {
            RepoVivienda rv = new RepoVivienda();
            bool imported = rv.import();

            string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Ha ocurrido un error, verifique el archivo Errores.txt" };

            if (imported)
            {
                message[0] = "alert-success";
                message[2] = "Barrios importados correctamente";
            }

            ViewBag.message = message;

            return View("Index", rv.findAll());
        }
    }
}
