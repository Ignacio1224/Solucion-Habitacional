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
        Contexto db = new Contexto();

        public bool add(Barrio b)
        {
            if (b == null) return false;
            try
            {
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
            if (db.barrio.Count() > 0)
            {
                var listaBarrios = from Barrio b in db.barrio select b;
                db.Dispose();

                return listaBarrios.ToList();
            }
            else return null;
        }

        public Barrio findByName(string bName)
        {
            try
            { 
                Barrio barrioBuscado = db.barrio.Find(bName);
                db.Dispose();
                if (barrioBuscado != null) return barrioBuscado;
                else return null;
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
