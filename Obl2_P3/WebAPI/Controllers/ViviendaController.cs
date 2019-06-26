using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio.Repositorios;
using Dominio.Clases;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //[RoutePrefix("api/Vivienda")]
    public class ViviendaController : ApiController
    {
        RepoVivienda rv = new RepoVivienda();

        //GET: <server>/api/GetByManyBedrooms/{cantDormitorios}
        //[Route("GetByManyBedrooms/{cantDormitorios:int}")]
        [HttpGet]
        public IHttpActionResult GetByManyBedrooms(int cantDormitorios)
        {
            if (cantDormitorios < 1) return BadRequest();

            var vLista = rv.findAll();

            if (vLista != null)
            {
                return Ok(VMViviendaAPI.ConvertToVMViviendaAPI(vLista));
            }
            else return NotFound();
        }

        //GET: <server>/api/GetByPriceRange/{pMin}/{pMax}}
        //[Route("GetByPriceRange/{pMin:decimal}/{pMax:decimal?}")]
        [HttpGet]
        public IHttpActionResult GetByPriceRange(decimal pMin, decimal pMax)
        {
            if (pMin < 0) return BadRequest();

            if (pMin > pMax)
            {
                decimal aux = pMin;
                pMin = pMax;
                pMax = aux;
            }

            RepoParametro rp = new RepoParametro();

            var listaV = from v in rv.findAll()
                         where (v.precio_final / rp.findByName(v.moneda).valor) >= pMin && (v.precio_final / rp.findByName(v.moneda).valor <= pMax)
                         select v;

            if (listaV != null)
            {
                return Ok(VMViviendaAPI.ConvertToVMViviendaAPI(listaV));
            }
            else return NotFound();
        }

        //GET: <server>/api/GetByBarrio/{idBarrio}
        //[Route("GetByBarrio/{idBarrio:int}")]
        [HttpGet]
        public IHttpActionResult GetByBarrio(int idBarrio)
        {
            RepoBarrio rb = new RepoBarrio();
            Barrio aux = rb.findById(idBarrio);

            if (aux == null) return BadRequest();

            var listaV = from v in rv.findAll()
                         where v.Barrio.BarrioId == idBarrio
                         select v;

            if (listaV != null)
            {
                return Ok(VMViviendaAPI.ConvertToVMViviendaAPI(listaV));
            }
            else return NotFound();
        }

        //GET: <server>/api/Vivienda/GetByState/{state}
        //[Route("GetByState/{state:int}")]
        [HttpGet]
        public IHttpActionResult GetByState(string state)
        {
            if (state == "-1") return BadRequest();

            var listaV = from v in rv.findAll()
                         where v.estado.ToString() == state
                         select v;

            if (listaV != null)
            {
                return Ok(VMViviendaAPI.ConvertToVMViviendaAPI(listaV));
            }
            else return NotFound();
        }

        //GET: <server>/api/GetByType/{type:alpha}
        //[Route("GetByType/{type:alpha}")]
        [HttpGet]
        public IHttpActionResult GetByType(string type)
        {
            if (type == "-1") return BadRequest();

            var listaV = from v in rv.findAll()
                         where v.ReturnType() == type
                         select v;

            if (listaV != null)
            {
                return Ok(VMViviendaAPI.ConvertToVMViviendaAPI(listaV));
            }
            else return NotFound();
        }

    }
}
