using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Interfaces;
using Dominio.Contexto_DB;

namespace Dominio.Repositorios
{
    public class RepoPostulante : IRepoPostulante
    {
        Contexto db = new Contexto();

        public bool add(Postulante p)
        {
            if (!p.esValido() || p == null) return false;

            try
            {
                db.postulantes.Add(p);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Postulante> findAll()
        {
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

        public Postulante findByCi(int pCi)
        {
            return db.postulantes.Find(pCi);
        }

        public bool login(Postulante p)
        {
            if (!p.esValido() || p == null) return false;

            Postulante post = db.postulantes.Find(p.cedula);

            if (post != null) return true;

            return false;
        }

        public bool update(Postulante p)
        {
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
                    pBuscado.fechaNac = p.fechaNac;
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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
