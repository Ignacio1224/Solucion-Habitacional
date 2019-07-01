using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Repositorios;
using Obl2_P3.Utilities;

namespace Obl2_P3.Controllers
{
    public class BarrioController : Controller
    {
        #region RepoInstances
        RepoBarrio rb = new RepoBarrio();
        #endregion

        #region Logic

        #region GET

        // GET: Barrio
        public ActionResult Index()
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            ViewBag.message = new string[] { "d-none", "", "" };

            return View(rb.findAll());
        }

        // GET: Barrio/Details/5
        public ActionResult Details(int id)
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            return View("Details", rb.findById(id));
        }

        #endregion

        #region POST

        // POST: Barrio/Import
        [HttpPost]
        public ActionResult Import()
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            bool imported = rb.import();

            string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Ha ocurrido un error, verifique el archivo Errores.txt" };

            if (imported)
            {
                message[0] = "alert-success";
                message[2] = "Barrios importados correctamente";
            }

            ViewBag.message = message;

            return View("Index", rb.findAll());
        }

        #endregion

        #endregion
    }
}
