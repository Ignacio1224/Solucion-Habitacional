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
    public class RepoSorteo : IRepoSorteo
    {
        Contexto db = new Contexto();

        public bool add(Sorteo s)
        {
            if (!s.esValido() || s == null) return false;

            try
            {
                db.sorteo.Add(s);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool delete(Sorteo s)
        {
            if (s == null) return false;

            try
            {
                db.sorteo.Remove(s);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<Sorteo> findAll()
        {
            List<Sorteo> sLista = null;

            try
            {
                if (db.sorteo.Count() > 0)
                {
                    sLista = (from Sorteo s in db.sorteo select s).ToList();
                    db.Dispose();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return sLista;

        }

        public Sorteo findById(int sId)
        {
            return db.sorteo.Find(sId);
        }

        public bool update(Sorteo s)
        {
            if (!s.esValido() || s == null) return false;
            try
            {
                Sorteo sBuscado = db.sorteo.Find(s.id);
                if (sBuscado != null)
                {
                    sBuscado.fecha = s.fecha;
                    sBuscado.ganador = s.ganador;
                    sBuscado.id = s.id;
                    sBuscado.postulantes = s.postulantes;
                    sBuscado.realizado = s.realizado;
                    sBuscado.vivienda = s.vivienda;

                    if (sBuscado.esValido())
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
