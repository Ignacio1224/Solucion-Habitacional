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
        RepoVivienda rv = new RepoVivienda();

        // GET: Vivienda
        public ActionResult Index()
        {
            ViewBag.message = new string[] { "d-none", "", "" };

            return View(VMVivienda.ConvertToVMVivienda(rv.findAll()));
        }

        // GET: Vivienda/Details/5
        public ActionResult Details(int id)
        {
            return View("Details", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
        }

        // POST: Vivienda/Import
        [HttpPost]
        public ActionResult Import()
        {
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

        //GET: Vivienda/UnfoldVivienda
        public ActionResult Unfold()
        {
            return View(VMVivienda.ConvertToVMVivienda(rv.findAll()));
        }

        // POST: Vivienda/ModifyState
        [HttpPost]
        public ActionResult ModifyState(int? id)
        {
            //no pude controlar aca
            if (id == null)
            {
                ViewBag.Message = "Debes seleccionar una vivienda.";
                return View("Unfold", VMVivienda.ConvertToVMVivienda(rv.findAll()));
            }
            return View("ModifyState", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
        }

        //POST: Vivienda/SaveModifyState
        [HttpPost]
        public ActionResult SaveModifyState(int id, string nuevoEstado)
        {
            //aca tampoco
            if (nuevoEstado == null)
            {
                ViewBag.Message = "Debes seleccionar un tipo de estado.";
                return View("ModifyState", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
            }

            Vivienda vivienda = rv.findById(id);

            //Programación 1
            if (nuevoEstado == "Inhabilitada")
            {
                vivienda.estado = Vivienda.Estados.Inhabilitada;
            }
            else if (nuevoEstado == "Recibida")
            {
                vivienda.estado = Vivienda.Estados.Recibida;
            }
            else if (nuevoEstado == "Sorteada")
            {
                vivienda.estado = Vivienda.Estados.Sorteada;
            }
            else if (nuevoEstado == "Habilitada")
            {
                vivienda.estado = Vivienda.Estados.Habilitada;
            }

            rv.update(vivienda);

            ViewBag.Success = "Estado modificado con éxito.";
            return View("Details", VMVivienda.ConvertToVMVivienda(vivienda));
        }

    }
}
