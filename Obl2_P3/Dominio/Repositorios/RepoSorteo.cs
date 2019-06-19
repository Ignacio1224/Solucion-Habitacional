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
        public bool add(Sorteo s)
        {
            Contexto db = new Contexto();

            if (!s.esValido() || s == null) return false;

            try
            {
                db.sorteos.Add(s);
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
            Contexto db = new Contexto();

            if (s == null) return false;

            try
            {
                db.sorteos.Remove(s);
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
            Contexto db = new Contexto();

            List<Sorteo> sLista = null;

            try
            {
                if (db.sorteos.Count() > 0)
                {
                    sLista = (from Sorteo s in db.sorteos select s).ToList();
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
            Contexto db = new Contexto();

            return db.sorteos.Find(sId);
        }

        public bool update(Sorteo s)
        {
            Contexto db = new Contexto();

            if (!s.esValido() || s == null) return false;
            try
            {
                Sorteo sBuscado = db.sorteos.Find(s.SorteoId);
                if (sBuscado != null)
                {
                    sBuscado.fecha = s.fecha;
                    sBuscado.Ganador = s.Ganador;
                    sBuscado.SorteoId = s.SorteoId;
                    sBuscado.Postulantes = s.Postulantes;
                    sBuscado.realizado = s.realizado;
                    sBuscado.Vivienda = s.Vivienda;

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
