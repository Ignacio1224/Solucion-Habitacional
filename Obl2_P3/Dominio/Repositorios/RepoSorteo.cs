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

        public bool update(Sorteo s)
        {

            if (!s.esValidoParaSortear() || s == null) return false;
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

                    if (sBuscado.esValidoParaSortear())
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
                db = new Contexto();
                if (db.viviendas.Count() > 0)
                {
                    sLista = (from Sorteo s in db.sorteos select s).Include(s => s.Vivienda).Include(s => s.Postulantes).Include(s => s.Ganador).Include(so => so.Vivienda).ToList();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return sLista;
            }

            return sLista;

        }

        public Sorteo findById(int sId)
        {
            Sorteo s = db.sorteos.Where(so => so.SorteoId == sId).Include(so => so.Postulantes).Include(so => so.Ganador).Include(so => so.Vivienda).FirstOrDefault();
            db.Dispose();
            return s;
        }

        public bool raffle(Sorteo s)
        {
            try
            {

                Random r = new Random();
                List<Postulante> pAux = s.Postulantes.OrderBy(px => px.apellido).ToList();
                bool reSortear = true;
                Postulante ganador = new Postulante();
                while (reSortear)
                {
                    reSortear = false;

                    ganador = pAux[r.Next(s.Postulantes.Count - 1)];


                    if (ganador.adjudicatario)
                    {
                        reSortear = true;
                    }
                }

                Contexto db = new Contexto();
                Postulante p = db.postulantes.Where(po => po.cedula == ganador.cedula).Include(po => po.Sorteo).Include(po => po.Sorteos).FirstOrDefault();
                Sorteo sAux = db.sorteos.Where(ss => ss.SorteoId == s.SorteoId).Include(ss => ss.Ganador).Include(ss => ss.Postulantes).Include(ss => ss.Vivienda).FirstOrDefault();

                p.Sorteo = sAux;
                db.Entry(p.Sorteo).State = EntityState.Modified;

                p.adjudicatario = true;
                db.Entry(p).State = EntityState.Modified;

                foreach (var item in p.Sorteos)
                    db.Entry(item).State = EntityState.Unchanged;

                sAux.Ganador = p;
                db.Entry(sAux.Ganador).State = EntityState.Modified;

                sAux.realizado = true;
                sAux.GanadorId = p.UsuarioId;
                db.Entry(sAux).State = EntityState.Modified;

                foreach (var item in sAux.Postulantes)
                    db.Entry(item).State = EntityState.Unchanged;
                db.Entry(sAux.Vivienda).State = EntityState.Unchanged;

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

        public bool inscribePostulanteAtSorteo(Postulante p, Sorteo s)
        {
            if (p == null || s == null) return false;
            if (!p.esValido() || !s.esValido()) return false;

            db = new Contexto();
            Sorteo sAux = db.sorteos.Where(sa => sa.SorteoId == s.SorteoId).Include(sa => sa.Postulantes).FirstOrDefault();

            if (sAux != null)
            {
                try
                {

                    sAux.Postulantes.Add(db.postulantes.Where(pd => pd.UsuarioId == p.UsuarioId).FirstOrDefault());
                    foreach (var item in sAux.Postulantes)
                        db.Entry(item).State = EntityState.Modified;
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
            return false;
        }

    }
}
