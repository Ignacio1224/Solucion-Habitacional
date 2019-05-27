using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Interfaces;
using Dominio.Contexto_DB;

namespace Dominio.Repo
{
    public class RepoPostulante : IRepoPostulante
    {
        Contexto db = new Contexto();

        public bool add(Postulante p)
        {
            if (!p.validar() || p == null) return false;
            try
            {
                db.postulantes.Add(p);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }

        public bool delete(Postulante p)
        {
            if (p == null) return false;
            try
            {
                db.postulantes.Remove(p);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }

        public IEnumerable<Postulante> findAll()
        {
            try
            {
                if (db.postulantes.Count() > 0)
                {
                    var listaPostulantes = from Postulante p in db.postulantes select p;
                    db.Dispose();

                    return listaPostulantes.ToList();
                }
                else return null;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return null;
            }
        }

        public Postulante findByCi(int pCi)
        {
            Postulante p = db.postulantes.Find(pCi);
            if (p != null) return p;
            else return null;
        }

        public bool login(Postulante p)
        {
            if (!p.validar() || p == null) return false;
            Postulante post = db.postulantes.Find(p.cedula);
            if (post != null) return true;
            else return false;
        }

        public bool update(Postulante p)
        {
            if (!p.validar() || p == null) return false;
            try
            {
                Postulante pBuscado = db.postulantes.Find(p.cedula);
                if (pBuscado != null)
                {
                    pBuscado.nombre = p.nombre;
                    pBuscado.apellido = p.apellido;
                    pBuscado.clave = p.clave;
                    pBuscado.email = p.email;
                    pBuscado.fechaNac = p.fechaNac;
                    if (pBuscado.validar())
                    {
                        db.SaveChanges();
                        db.Dispose();
                        return true;
                    }
                    else return false;
                }
                else return false;

            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }
    }
}
