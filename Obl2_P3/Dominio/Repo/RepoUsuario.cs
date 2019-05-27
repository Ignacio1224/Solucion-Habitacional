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
    public class RepoUsuario : IRepoUsuario
    {
        Contexto db = new Contexto();

        public bool add(Usuario u)
        {
            if (!u.validar() || u == null) return false;
            try
            {
                db.usuario.Add(u);
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

        public bool delete(Usuario u)
        {
            if (u == null) return false;
            try
            {
                db.usuario.Remove(u);
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

        public IEnumerable<Usuario> findAll()
        {
            try
            {
                if (db.usuario.Count() > 0)
                {
                    var uLista = from Usuario u in db.usuario select u;
                    db.Dispose();

                    return uLista.ToList();
                }
                else return null;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return null;
            }
        }

        public Usuario findByCi(int uCi)
        {
            Usuario u = db.postulantes.Find(uCi);
            if (u != null) return u;
            else return null;
        }

        public bool login(Usuario u)
        {
            if (!u.validar() || u == null) return false;
            Usuario usu = db.usuario.Find(u.cedula);
            if (usu != null) return true;
            else return false;
        }

        public bool update(Usuario u)
        {
            if (!u.validar() || u == null) return false;
            try
            {
                Usuario uBuscado = db.usuario.Find(u.cedula);
                if (uBuscado != null)
                {
                    uBuscado.cedula = u.cedula;
                    uBuscado.clave = u.clave;
                    if (u.validar())
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
