using Dominio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using Dominio.Repositorios;
using Obl2_P3.Models;
using Obl2_P3.Utilities;

namespace Obl2_P3.Controllers
{
    public class ViviendaController : Controller
    {
        #region RepoInstances
        RepoVivienda rv = new RepoVivienda();
        #endregion

        #region Logic
        
        #region GET

        // GET: Vivienda
        public ActionResult Index()
        {
           // if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            ViewBag.message = new string[] { "d-none", "", "" };

            return View(VMVivienda.ConvertToVMVivienda(rv.findAll()));
        }

        // GET: Vivienda/Details/5
        public ActionResult Details(int id)
        {
            // if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            return View("Details", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
        }

        // GET: /Vivienda/Edit/{id}
        public ActionResult Edit(int id)
        {
            //  if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            string[] message = new string[] { "d-none", "padding: 1em; margin-bottom: 0.6em;", "" };
            ViewBag.message = message;
            return View("Edit", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
        }

        #endregion

        #region POST

        // POST: /Vivienda/Edit/{id}
        [HttpPost]
        public ActionResult Edit(VMVivienda vmv)
        {
            // if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Ha ocurrido un error!" };

            if (vmv.estado != VMVivienda.Estaditos.Sorteada && rv.update(VMVivienda.ConvertToVivienda(vmv)))
            {
                message[0] = "alert-success";
                message[2] = "Edición correcta!";
            }

            ViewBag.message = message;
            return View(vmv);
        }

        // POST: Vivienda/Import
        [HttpPost]
        public ActionResult Import()
        {
            // if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Ha ocurrido un error, verifique el archivo Errores.txt" };

            if (rv.import())
            {
                message[0] = "alert-success";
                message[2] = "Viviendas importadas correctamente";
            }

            ViewBag.message = message;

            return View("Index", VMVivienda.ConvertToVMVivienda(rv.findAll()));
        }

        #endregion
        
        #endregion
    }
}
