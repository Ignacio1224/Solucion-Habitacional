using Dominio.Repositorios;
using Obl2_P3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obl2_P3.Controllers
{
    public class SorteoController : Controller
    {

        RepoSorteo rs = new RepoSorteo();
        RepoVivienda rv = new RepoVivienda();

        // GET: Sorteo
        public ActionResult Index()
        {
            return View(VMSorteo.ConvertToVMSorteo(rs.findAll()));
        }

        // GET: Sorteo/Details/{id}
        public ActionResult Details(int id)
        {
            return View(VMSorteo.ConvertToVMSorteo(rs.findById(id)));
        }

        // GET: Sorteo/Create
        public ActionResult Create()
        {

            ViewBag.viviendas = rv.findAllEnabled();
            return View();
        }

        // POST: Sorteo/Create
        [HttpPost]
        public ActionResult Create(VMSorteo vms)
        {
            try
            {
                rs.add(VMSorteo.ConvertToSorteo(vms));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sorteo/Edit/{id}
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sorteo/Edit/{id}
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sorteo/Delete/{id}
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sorteo/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Sorteo/Raffle
        [HttpPost]
        public ActionResult Raffle(int id)
        {
            // Sortear
            return RedirectToAction("Details", "Sorteo", new { id = id });

        }

    }
}
