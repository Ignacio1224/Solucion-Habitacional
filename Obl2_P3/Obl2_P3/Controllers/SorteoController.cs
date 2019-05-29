using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obl2_P3.Controllers
{
    public class SorteoController : Controller
    {
        // GET: Sorteo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sorteo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sorteo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sorteo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sorteo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sorteo/Edit/5
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

        // GET: Sorteo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sorteo/Delete/5
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
    }
}
