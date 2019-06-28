using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Repositorios;
using Obl2_P3.Utilities;

namespace Obl2_P3.Controllers
{
    public class ParametroController : Controller
    {
        #region RepoInstances
        RepoParametro rp = new RepoParametro();
        #endregion

        #region Logic

        #region GET

        // GET: Parametro
        public ActionResult Index()
        {
            //  if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            ViewBag.message = new string[] { "d-none", "", "" };

            return View(rp.findAll());
        }

        // GET: Parametro/Details/5
        public ActionResult Details(String id)
        {
            //  if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            return View("Details", rp.findByName(id));
        }

        #endregion

        #region POST

        // POST: Parametro/Import
        [HttpPost]
        public ActionResult Import()
        {
            //  if (!Check.UserLog()) return new HttpStatusCodeResult(401);

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

        #endregion

        #endregion
    }
}
