 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Repositorios;

namespace Obl2_P3.Controllers
{
    public class BarrioController : Controller
    {
        // GET: Barrio
        public ActionResult Index()
        {
            ViewBag.message = new string[] { "d-none", "", "" };

            RepoBarrio rb = new RepoBarrio();

            return View(rb.findAll());
        }

        // GET: Barrio/Details/5
        public ActionResult Details(int id)
        {
            RepoBarrio rb = new RepoBarrio();

            return View("Details", rb.findById(id));
        }

        // POST: Barrio/Import
        [HttpPost]
        public ActionResult Import()
        {
            RepoBarrio rb = new RepoBarrio();
            bool imported = rb.import();

            string[] message = new string[] {"alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Ha ocurrido un error, verifique el archivo Errores.txt" };

            if (imported)
            {
                message[0] = "alert-success";
                message[2] = "Barrios importados correctamente";
            }

            ViewBag.message = message;

            return View("Index", rb.findAll());
        }
    }
}
