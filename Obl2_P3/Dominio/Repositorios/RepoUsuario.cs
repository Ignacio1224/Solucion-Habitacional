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
    public class RepoUsuario : IRepoUsuario
    {
        Contexto db = new Contexto();

        public bool add(Usuario u)
        {
            if (!u.esValido() || u == null) return false;

            try
            {
                db.usuario.Add(u);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                return false;
            }

        }

        public IEnumerable<Usuario> findAll()
        {
            List<Usuario> uLista = null;
            try
            {
                if (db.usuario.Count() > 0)
                {
                    uLista = (from Usuario u in db.usuario select u).ToList();
                    db.Dispose();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return uLista;

        }

        public Usuario findByCi(int uCi)
        {
            return db.postulantes.Find(uCi);
        }

        public bool login(Usuario u)
        {
            if (!u.esValido() || u == null) return false;

            Usuario usu = db.usuario.Find(u.cedula);
            if (usu != null) return true;
            else return false;
        }

        public bool update(Usuario u)
        {
            if (!u.esValido() || u == null) return false;

            try
            {
                Usuario uBuscado = db.usuario.Find(u.cedula);
                if (uBuscado != null)
                {
                    uBuscado.cedula = u.cedula;
                    uBuscado.clave = u.clave;
                    if (u.esValido())
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
