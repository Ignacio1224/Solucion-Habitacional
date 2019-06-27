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
    public class RepoSorteo : IRepoSorteo
    {
        Contexto db = new Contexto();

        public bool add(Sorteo s)
        {
            bool added = false;
            Contexto db = new Contexto();
            RepoVivienda rv = new RepoVivienda();

            if (!s.esValido() || s == null) return added;

            try
            {

                s.Vivienda.estado = Vivienda.Estados.Sorteada;

                if (rv.update(s.Vivienda))
                {
                    db.Entry(s.Vivienda).State = EntityState.Unchanged;
                    if (s.Postulantes != null)
                    {
                        db.Entry(s.Postulantes).State = EntityState.Unchanged;
                    }

                    if (s.Ganador != null)
                    {
                        db.Entry(s.Ganador).State = EntityState.Unchanged;
                    }

                    db.sorteos.Add(s);
                    db.SaveChanges();
                    db.Dispose();
                    added = true;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }

            return added;
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
            List<Sorteo> sLista = new List<Sorteo>();

            try
            {
                if (db.viviendas.Count() > 0)
                {
                    sLista = (from Sorteo s in db.sorteos select s).Include(s => s.Vivienda).Include(s => s.Postulantes).Include(s => s.Ganador).Include(so => so.Vivienda).ToList();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {

            }

            return sLista;

        }

        public Sorteo findById(int sId)
        {
            Sorteo s = db.sorteos.Where(so => so.SorteoId == sId).Include(so => so.Postulantes).Include(so => so.Ganador).Include(so => so.Vivienda).FirstOrDefault();
            db.Dispose();
            return s;
        }

        public Sorteo raffle(Sorteo s)
        {
            s.sortear();
            bool addedd = add(s);
            return s;
            
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
