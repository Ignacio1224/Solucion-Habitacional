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
using System.Net.Http.Headers;

namespace Obl2_P3.Controllers
{
    public class ViviendaController : Controller
    {
        #region RepoInstances
        RepoVivienda rv = new RepoVivienda();
        #endregion

        #region ApiCommands

        HttpClient client = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        Uri productoUri = null;

        public ViviendaController()
        {
            client.BaseAddress = new Uri("http://localhost:50310");

            productoUri = new Uri("http://localhost:50310/api/");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

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

        // GET: Vivienda/Edit/{id}
        public ActionResult Edit(int id)
        {
            //  if (!Check.UserLog()) return new HttpStatusCodeResult(401);

            string[] message = new string[] { "d-none", "padding: 1em; margin-bottom: 0.6em;", "" };
            ViewBag.message = message;
            return View("Edit", VMVivienda.ConvertToVMVivienda(rv.findById(id)));
        }

        // GET: Vivienda/GetByBarrio/{idBarrio} -> api/GetByBarrio/{idBarrio}
        public ActionResult GetByBarrio(int idBarrio)
        {
            
            response = client.GetAsync(productoUri + "GetByBarrio/" + idBarrio).Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Vivienda>>().Result;
                List<VMVivienda> listVM = VMVivienda.ConvertToVMVivienda(list);
                if (listVM.Count > 0)
                {
                    ViewBag.message = "Success.";
                }else
                {
                    ViewBag.message = "El barrio indicado no posee viviendas ingresadas.";
                }
                return View(listVM);
            }
            else
            {
                ViewBag.message = "Petición no encontrada";
                return View();
            }
        }

        // GET: Vivienda/GetByPriceRange/{pMin}/{pMax} -> api/GetByPriceRange/{pMin}/{pMax}
        public ActionResult GetByPriceRange(decimal pMin, decimal pMax)
        {
            response = client.GetAsync(productoUri +"GetByPriceRange/" + pMin +"/"+pMax).Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Vivienda>>().Result;
                List<VMVivienda> listVM = VMVivienda.ConvertToVMVivienda(list);
                if (listVM.Count > 0)
                {
                    ViewBag.message = "Success.";
                }
                else
                {
                    ViewBag.message = "No existen viviendas entre el rango de precio indicado.";
                }
                return View(listVM);
            }
            else
            {
                ViewBag.message = "Petición no encontrada";
                return View();
            }
        }

        // GET: Vivienda/GetByManyBedrooms/{cantDorm} -> api/GetByManyBedrooms/{cantDorm}
        public ActionResult GetByManyBedrooms(int cantDorm)
        {
            response = client.GetAsync(productoUri + "GetByManyBedrooms/" + cantDorm).Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Vivienda>>().Result;
                List<VMVivienda> listVM = VMVivienda.ConvertToVMVivienda(list);

                if (listVM.Count > 0)
                {
                    ViewBag.message = "Success.";
                }
                else
                {
                    ViewBag.message = "No existen viviendas con la cantidad de dormitorios solicitada.";
                }
                return View(listVM);
            }
            else
            {
                ViewBag.message = "Petición no encontrada";
                return View();
            }

        }

        // GET: Vivienda/GetByState/{state} -> api/GetByState/{state}
        public ActionResult GetByState(int state)
        {
            response = client.GetAsync(productoUri + "GetByState/" + state ).Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Vivienda>>().Result;
                List<VMVivienda> listVM = VMVivienda.ConvertToVMVivienda(list);
                if (listVM.Count > 0)
                {
                    ViewBag.message = "Success.";
                }
                else
                {
                    ViewBag.message = "No hay viviendas para mostrar.";
                }
                return View(listVM);
            }
            else
            {
                ViewBag.message = "Petición no encontrada";
                return View(new VMVivienda());
            }
        }

        // GET: Vivienda/GetByType/{type} -> api/GetByType/{type}
        public ActionResult GetByType(string type)
        {
            response = client.GetAsync(productoUri + "GetByType/" + type).Result;
            if (response.IsSuccessStatusCode)
            {
                var list = response.Content.ReadAsAsync<IEnumerable<Vivienda>>().Result;
                List<VMVivienda> listVM = VMVivienda.ConvertToVMVivienda(list);
                if (listVM.Count > 0)
                {
                    ViewBag.message = "Success.";
                }
                else
                {
                    ViewBag.message = "No hay viviendas para mostrar.";
                }
                return View(listVM);
            }
            else
            {
                ViewBag.message = "Petición no encontrada";
                return View(new VMVivienda());
            }
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
