using Dominio.Repositorios;
using Obl2_P3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Create()
        {

            ViewBag.viviendas = Enumerable.Empty<Dominio.Clases.Vivienda>().ToList();
            //ViewBag.viviendas = rv.findAllEnabled();
            ViewBag.barrios = rb.findAll();
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

        public ActionResult ViviendaPorBarrio(int id)
        {
            //HttpClient client = new HttpClient();
            //HttpResponseMessage res = new HttpResponseMessage();
            //string url = null;

            //client.BaseAddress = new Uri("http://localhost:50265/api/");
            //url = "GetByBarrio/" + id;

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //res = client.GetAsync(url).Result;
            //List<VMVivienda> vs = new List<VMVivienda>();

            //if (res.IsSuccessStatusCode)
            //{
            //    vs = res.Content.ReadAsAsync<IEnumerable<VMVivienda>>().Result.ToList();
            //}

            //ViewBag.viviendas = res.Content.ReadAsAsync<IEnumerable<VMVivienda>>().Result;
            ViewBag.viviendas = rv.findByBarrio(id);
            ViewBag.barrios = rb.findAll();
            return View("Create", "Sorteo");
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
