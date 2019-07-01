using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Dominio.Repositorios;
using Obl2_P3.Models;
using Obl2_P3.Utilities;
using Dominio.Clases;


namespace Obl2_P3.Controllers
{
    public class SorteoController : Controller
    {
        #region Instancias repo
        RepoSorteo rs = new RepoSorteo();
        RepoVivienda rv = new RepoVivienda();
        RepoBarrio rb = new RepoBarrio();
        #endregion

        #region Logica

        #region GET

        // GET: Sorteo
        public ActionResult Index()
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);


            ViewBag.message = new string[] { "d-none", "", "" };

            return View(VMSorteo.ConvertToListVMSorteo(rs.findAll().ToList()));
        }

        // GET: Sorteo/Details/{id}
        public ActionResult Details(int id)
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            return View(VMSorteo.ConvertToVMSorteo(rs.findById(id)));
        }

        // GET: Sorteo/Create
        public ActionResult CreatePreSorteo()
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            ViewBag.barrios = new SelectList(rb.findAll(), "BarrioId", "nombre_barrio");
            return View();
        }

        // GET: Sorteo/CreateSorteo
        public ActionResult CreateSorteo(int id)
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            ViewBag.viviendas = new SelectList(rv.findByBarrioToRaffle(id), "ViviendaId", "ViviendaId");
            VMSorteo vms = new VMSorteo();
            vms.BarrioId = id;
            string[] message = new string[] { "d-none", "padding: 1em; margin-bottom: 0.6em;", "" };

            ViewBag.message = message;

            return View(vms);
        }

        #endregion

        #region POST

        // POST: Sorteo/Create
        [HttpPost]
        public ActionResult CreatePreSorteo(VMSorteo vms)
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            if (vms.BarrioId != 0)
            {
                return RedirectToAction("CreateSorteo/" + vms.BarrioId);
            }

            return View();
        }


        // POST: Sorteo/CreateSorteo
        [HttpPost]
        public ActionResult CreateSorteo(VMSorteo vms)
        {
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            string[] message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "Ha ocurrido un error, verifique los datos" };

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
            if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            VMSorteo vms = VMSorteo.ConvertToVMSorteo(rs.findById(id));

            if (vms.Postulantes.Count() == 0)
            {
                ViewBag.message = new string[] { "alert-danger", "padding: 1em; margin-bottom: 0.6em;", "El sorteo no puede ser realizado ya que no posee postulantes inscriptos." };

                // cuando retorna la vista no manda el model con la vm convertida
                return View("Index", VMSorteo.ConvertToListVMSorteo(rs.findAll().ToList()));
            }

            // Sortear
            vms = VMSorteo.ConvertToVMSorteo(rs.raffle(VMSorteo.ConvertToSorteo(vms)));

            return RedirectToAction("Details", "Sorteo", new { id = id });
        }


        // POST: Sorteo/InscribePostulanteAtSorteo
        [HttpPost]
        public ActionResult InscribePostulanteAtSorteo(int id)
        {
            Postulante p = Session["userLog"] as Postulante;
            int sId = rs.findById(id).SorteoId;

            try
            {
                if (rs.inscribePostulanteAtSorteo(p, sId))
                {
                    TempData["msj"] = "Te has inscripto con éxito";
                    return View("Index",VMSorteo.ConvertToListVMSorteo(rs.findAll().ToList() ) ) ;
                };

                TempData["msj"] = "No has podido inscribirte correctamente.";
                return View("Index", VMSorteo.ConvertToListVMSorteo(rs.findAll().ToList()));

            }
            catch (Exception)
            {
                TempData["msj"] = "Error inesperado.";

                return View("Index");
            }
        }
        #endregion

        #endregion
    }
}
