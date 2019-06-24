using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio.Repositorios;
using Dominio.Clases;

namespace WebAPI.Controllers
{
    [RoutePrefix("API_SH/Vivienda")]
    public class ViviendaController : ApiController
    {
        RepoVivienda rv = new RepoVivienda();

        public IHttpActionResult GetHowManyBedrooms(int cantDormitorios)
        {
            if (cantDormitorios < 1) return NotFound();

            var listaV = from v in rv.findAll()
                         where v.cant_dormitorio == cantDormitorios
                         select v;

            return Ok(listaV);
        }

        public IHttpActionResult GetByPriceRange(decimal pMin, decimal pMax)
        {
            if (pMin < 0) return NotFound();

            if (pMin > pMax)
            {
                decimal aux = pMax;
                pMax = pMin;
                pMin = aux;
            }

            RepoParametro rp = new RepoParametro();
            var listaV = from v in rv.findAll()
                         where (v.precio_final / rp.findByName(v.moneda).valor) >= pMin && (v.precio_final / rp.findByName(v.moneda).valor <= pMax)
                         select v;

            return Ok(listaV);
        }

        public IHttpActionResult GetByBarrio(int idBarrio)
        {
            RepoBarrio rb = new RepoBarrio();
            Barrio aux = rb.findById(idBarrio);

            if (aux == null) return NotFound();

            var listaV = from v in rv.findAll()
                         where v.Barrio == aux
                         select v;

            return Ok(listaV);
        }

        public IHttpActionResult GetByState(string state)
        {

            //if (state != Vivienda.Estados) return NotFound(); // Como recorrer los tipos del enum sin caer en programacion 1?

            var listaV = from v in rv.findAll()
                         where v.estado.ToString() == state
                         select v;

            return Ok(listaV);
        }

        public IHttpActionResult GetByType(string type)
        {

            if (type == "-1") return NotFound();

            var listaV = from v in rv.findAll()
                         where v.ReturnType() == type
                         select v;

            return Ok(listaV);
        }

    }
}
