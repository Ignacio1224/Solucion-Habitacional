using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obl2_P3.Controllers
{
    public class ParametroController : Controller
    {
        // GET: Parametro
        public ActionResult Index()
        {
            ViewBag.message = new string[] { "d-none", "", "" };

            RepoParametro rp = new RepoParametro();

            return View(rp.findAll());
        }

        // GET: Parametro/Details/5
        public ActionResult Details(String id)
        {
            RepoParametro rp = new RepoParametro();

            return View("Details", rp.findByName(id));
        }

        // POST: Parametro/Import
        [HttpPost]
        public ActionResult Import()
        {
            RepoParametro rp = new RepoParametro();
            bool imported = rp.import();

            string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Ha ocurrido un error, verifique el archivo Errores.txt" };

            if (imported)
            {
                message[0] = "alert-success";
                message[2] = "Parámetros importados correctamente";
            }

            ViewBag.message = message;

            return View("Index", rp.findAll());
        }
    }
}
