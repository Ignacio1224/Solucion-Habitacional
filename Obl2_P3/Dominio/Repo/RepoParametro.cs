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
    public class RepoParametro : IRepoParametro
    {
        Contexto db = new Contexto();

        public bool add(Parametro p)
        {
            if (p == null) return false;
            try
            {
                db.parametro.Add(p);
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

        public bool delete(Parametro p)
        {
            if (p == null) return false;
            try
            {
                db.parametro.Remove(p);
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

        public Parametro findByName(string pName)
        {
            try
            { 
                Parametro p = db.parametro.Find(pName);
                db.Dispose();
                if (p != null) return p;
                else return null;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return null;
            }
        }

        public bool update(Parametro p)
        {
            try
            {
                Parametro pBuscado = db.parametro.Find(p.nombre);
                if (pBuscado != null)
                {
                    pBuscado.valor = p.valor;
                    db.SaveChanges();
                    db.Dispose();
                    return true;
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
