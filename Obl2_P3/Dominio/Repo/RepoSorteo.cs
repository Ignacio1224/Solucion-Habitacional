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
    public class RepoSorteo : IRepoSorteo
    {
        Contexto db = new Contexto();

        public bool add(Sorteo s)
        {
            if (!s.validar() || s == null) return false;
            try
            {
                db.sorteo.Add(s);
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

        public bool delete(Sorteo s)
        {
            if (s == null) return false;
            try
            {
                db.sorteo.Remove(s);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }

        public IEnumerable<Sorteo> findAll()
        {
            try
            {
                if (db.sorteo.Count() > 0)
                {
                    var sLista = from Sorteo s in db.sorteo select s;
                    db.Dispose();

                    return sLista.ToList();
                }
                else return null;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return null;
            }
        }

        public Sorteo findById(int sId)
        {
            Sorteo s = db.sorteo.Find(sId);
            if (s != null) return s;
            else return null;
        }

        public bool update(Sorteo s)
        {
            if (!s.validar() || s == null) return false;
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
                    if (sBuscado.validar())
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
