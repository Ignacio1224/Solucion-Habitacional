using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Interfaces;
using Dominio.Contexto_DB;
using System.Data.Entity;

namespace Dominio.Repositorios
{
    public class RepoPostulante : IRepoPostulante
    {
        Contexto db = new Contexto();
        RepoUsuario ru = new RepoUsuario();

        public bool add(Postulante p)
        {
            Contexto db = new Contexto();
            bool added = false;

            if (!p.esValido() || p == null) return added;

            try
            {
                Usuario userPostulante = new Usuario()
                {
                    cedula = p.cedula,
                    clave = p.clave
                };

                if (ru.add(userPostulante))
                {
                    ru = new RepoUsuario();
                    p.UsuarioId = ru.findByCi(p.cedula).UsuarioId;

                    db.usuarios.Add(p);

                    db.SaveChanges();
                    db.Dispose();
                    added = true;
                }

            }
            catch (Exception ex)
            {
                string msj = ex.Message;
            }

            return added;
        }

        public bool delete(Postulante p)
        {
            Contexto db = new Contexto();

            if (p == null) return false;

            try
            {
                db.postulantes.Remove(p);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Postulante> findAll()
        {
            Contexto db = new Contexto();

            List<Postulante> listaPostulantes = null;
            try
            {
                if (db.postulantes.Count() > 0)
                {
                    listaPostulantes = (from Postulante p in db.postulantes select p).ToList();
                    db.Dispose();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return listaPostulantes;

        }

        public Postulante findByCi(string pCi)
        {
            return db.postulantes.Where(p => p.cedula == pCi).Include(p => p.Sorteos).Include(p => p.SorteoGanado).FirstOrDefault();
        }

        public bool validarLogin(Postulante p)
        {
            Contexto db = new Contexto();

            if (!p.esValido() || p == null) return false;

            Postulante post = db.postulantes.Find(p.cedula);

            if (post != null) return true;

            return false;
        }

        public bool update(Postulante p)
        {
            Contexto db = new Contexto();

            if (!p.esValido() || p == null) return false;

            try
            {
                Postulante pBuscado = db.postulantes.Find(p.cedula);
                if (pBuscado != null)
                {
                    pBuscado.nombre = p.nombre;
                    pBuscado.apellido = p.apellido;
                    pBuscado.clave = p.clave;
                    pBuscado.email = p.email;
                    pBuscado.fecha_nac = p.fecha_nac;
                    pBuscado.adjudicatario = p.adjudicatario;
                    //pBuscado.SorteoGanado = p.SorteoGanado;
                    //pBuscado.Sorteos = p.Sorteos;
                    pBuscado.cedula = p.cedula;

                    if (pBuscado.esValido())
                    {
                        db.SaveChanges();
                        db.Dispose();
                        return true;
                    }
                    return false;
                }
                return false;

            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }

        public bool winnerAssignSorteo(int pId, Sorteo sorteo)
        {
            Postulante pAux = db.postulantes.Where(p => p.UsuarioId == pId).Include(p => p.SorteoGanado).SingleOrDefault();

            //guardamos el nuevo valor de adjudicatario
            pAux.adjudicatario = true;
            update(pAux);

            pAux.SorteoGanado = sorteo;
            //Le avisamos a Entity tenga en cuenta q el objeto ha sido modificado
            db.Entry(pAux).State = EntityState.Modified;

            db.SaveChanges();
            db.Dispose();
            return true;
        }
    }
}
