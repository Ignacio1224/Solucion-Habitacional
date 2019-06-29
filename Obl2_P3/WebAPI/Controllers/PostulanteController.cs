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
    //[RoutePrefix("api/Postulante")]
    public class PostulanteController : ApiController
    {
        RepoPostulante rp = new RepoPostulante();
        RepoUsuario ru = new RepoUsuario();

        //POST: <server>/api/RegisterPostulante
        [HttpPost]
        public IHttpActionResult RegisterPostulante([FromBody] VMPostulanteAPI p)
        {
            if (ModelState.IsValid)
            {
                Postulante pAux = rp.findByCi(p.cedula);

                if (pAux == null)
                {
                    if (rp.add(VMPostulanteAPI.ConvertToPostulante(p)) && ru.add(new Usuario() {cedula = p.cedula, clave = p.clave}))
                    {
                        return Ok();
                    }
                    else return InternalServerError();
                }
                else return BadRequest();
            }
            else return NotFound();
        }
    }
}

