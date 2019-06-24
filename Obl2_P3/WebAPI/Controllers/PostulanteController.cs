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
    [RoutePrefix("API_SH/Postulante")]
    public class PostulanteController : ApiController
    {
        RepoPostulante rp = new RepoPostulante();
        RepoUsuario ru = new RepoUsuario();

        // Postulante/POST
        public IHttpActionResult Post(Postulante p)
        {
            Postulante pAux = rp.findByCi(p.cedula);

            if (pAux != null)
            {
                Usuario u = new Usuario()
                {
                    cedula = p.cedula,
                    clave = p.clave
                };

                ru.add(u);
                return Ok();
            }

            else return NotFound();
        }

    }
}
