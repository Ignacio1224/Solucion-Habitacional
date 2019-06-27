using Dominio.Repositorios;
using Obl2_P3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Dominio.Clases;

namespace Obl2_P3.Controllers
{
    public class SorteoController : Controller
    {

        RepoSorteo rs = new RepoSorteo();
        RepoVivienda rv = new RepoVivienda();
        RepoBarrio rb = new RepoBarrio();

        // GET: Sorteo
        public ActionResult Index()
        {
            ViewBag.message = new string[] { "d-none", "", "" };
            return View(VMSorteo.ConvertToVMSorteo(rs.findAll()));
        }

        // GET: Sorteo/Details/{id}
        public ActionResult Details(int id)
        {
            return View(VMSorteo.ConvertToVMSorteo(rs.findById(id)));
        }

        // GET: Sorteo/Create
        public ActionResult CreatePreSorteo()
        {
            ViewBag.barrios = new SelectList(rb.findAll(), "BarrioId", "nombre_barrio");
            return View();
        }


        // POST: Sorteo/Create
        [HttpPost]
        public ActionResult CreatePreSorteo(VMSorteo vms)
        {
            if (vms.BarrioId != 0)
            {
                return RedirectToAction("CreateSorteo/" + vms.BarrioId);
            }

            return View();
        }

        // GET: Sorteo/CreateSorteo
        public ActionResult CreateSorteo(int id)
        {
            ViewBag.viviendas = new SelectList(rv.findByBarrioToRaffle(id), "ViviendaId", "ViviendaId");
            VMSorteo vms = new VMSorteo();
            vms.BarrioId = id;
            string[] message = new string[] { "d-none", "padding: 1em; margin-bottom: 0.6em;", "" };

            ViewBag.message = message;

            return View(vms);
        }

        // POST: Sorteo/CreateSorteo
        [HttpPost]
        public ActionResult CreateSorteo(VMSorteo vms)
        {
            string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "El sorteo no posee postulantes" };

            if (rs.add(VMSorteo.ConvertToSorteo(vms)))
            {
                message[0] = "alert-success";
                message[2] = "Operacion correcta";
            }


            ViewBag.viviendas = new SelectList(rv.findByBarrioToRaffle(vms.BarrioId), "ViviendaId", "ViviendaId");
            ViewBag.message = message;

            return View(vms);
        }


        // POST: Sorteo/Raffle
        [HttpPost]
        public ActionResult Raffle(int id)
        {
            RepoSorteo rss = new RepoSorteo();
            VMSorteo vms = new VMSorteo();
            vms = VMSorteo.ConvertToVMSorteo(rs.findById(id));

            if (vms.Postulantes.Count() == 0)
            {
                string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "El sorteo no posee postulantes" };
                ViewBag.message = message;
                return View("Index", VMSorteo.ConvertToVMSorteo(rss.findAll()));
            }

            // Sortear
            vms = VMSorteo.ConvertToVMSorteo(rss.raffle(VMSorteo.ConvertToSorteo(vms)));

            return RedirectToAction("Details", "Sorteo", new { id = id });
        }
    }
}
