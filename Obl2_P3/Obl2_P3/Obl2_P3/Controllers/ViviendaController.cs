﻿using Dominio.Repositorios;
using Dominio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obl2_P3.Models;
using System.Net.Http;


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

        // GET: /Vivienda/Edit/{id}
        public ActionResult Edit(int id)
        {
            return View("Edit", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
        }

        // POST: /Vivienda/Edit/{id}
        [HttpPost]
        public ActionResult Edit(VMVivienda vmv)
        {
            rv.update(VMVivienda.ConvertToVivienda(vmv));
            ViewBag.Message = "Se ha cambiado el estado exitosamente";
            return View(vmv);
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


    }
}
