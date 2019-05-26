using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Contexto_DB;
using Dominio.Interfaces;

namespace Dominio.Repo
{
    public class RepoBarrio : IRepoBarrio
    {
        public bool add(Barrio b)
        {
            if (b == null) return false;

            try
            {
                Contexto db = new Contexto();
                db.barrio.Add(b);
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

        public bool delete(Barrio b)
        {
            if (b == null) return false;

            try
            {
                Contexto db = new Contexto();
                db.barrio.Remove(b);
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

        public IEnumerable<Barrio> findAll()
        {
            Contexto db = new Contexto();
            var listaBarrios = from Barrio b in db.barrio select b;
            db.Dispose();

            return listaBarrios.ToList();
        }

        public Barrio findByName(string bName)
        {
            try
            {
                Contexto db = new Contexto();
                Barrio barrioBuscado = db.barrio.Find(bName);
                db.Dispose();
                if (barrioBuscado != null) return barrioBuscado;
                return null;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return null;
            }
        }

        public bool update(Barrio b)
        {
            try
            {
                Contexto db = new Contexto();
                Barrio barrioBuscado = db.barrio.Find(b.nombre);
                if (barrioBuscado != null)
                {
                    barrioBuscado.descripcion = b.descripcion;
                    db.SaveChanges();
                    db.Dispose();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }
    }
}
