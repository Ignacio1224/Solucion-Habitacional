using Dominio.Repositorios;
using Dominio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obl2_P3.Models;

namespace Obl2_P3.Controllers
{
    public class ViviendaController : Controller
    {
        // GET: Vivienda
        public ActionResult Index()
        {
            ViewBag.message = new string[] { "d-none", "", "" };

            RepoVivienda rv = new RepoVivienda();

            return View(VMVivienda.ConvertToVMVivienda(rv.findAll()));
        }

        // GET: Vivienda/Details/5
        public ActionResult Details(int id)
        {
            RepoVivienda rv = new RepoVivienda();

            return View("Details", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
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
                message[2] = "Viviendas importadas correctamente";
            }

            ViewBag.message = message;

            return View("Index", VMVivienda.ConvertToVMVivienda(rv.findAll()));
        }

        // GET: Vivienda/Edit
        public ActionResult Edit(int id)
        {
            RepoVivienda rv = new RepoVivienda();

            return View("Edit", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
        }

        // POST: Vivienda/Edit
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return View();
        }

    }
}
