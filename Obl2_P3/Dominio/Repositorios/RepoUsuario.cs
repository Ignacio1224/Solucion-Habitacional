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
        

        public bool add(Usuario u)
        {
            Contexto db = new Contexto();

            if (!u.esValido() || u == null) return false;

            try
            {
                db.usuarios.Add(u);
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
            Contexto db = new Contexto();

            if (u == null) return false;

            try
            {
                db.usuarios.Remove(u);
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
            Contexto db = new Contexto();

            List<Usuario> uLista = null;
            try
            {
                if (db.usuarios.Count() > 0)
                {
                    uLista = (from Usuario u in db.usuarios select u).ToList();
                    db.Dispose();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return uLista;

        }

        public Usuario findByCi(string uCi)
        {
            Contexto db = new Contexto();

            return db.usuarios.Where(u => u.cedula == uCi.ToString()).FirstOrDefault();
        }

        public bool validarLogin(Usuario u)
        {
            Contexto db = new Contexto();

            if (!u.esValido() || u == null) return false;

            Usuario usu = db.usuarios.Find(u.cedula);
            if (usu != null) return true;
            else return false;
        }

        public bool update(Usuario u)
        {
            Contexto db = new Contexto();

            if (!u.esValido() || u == null) return false;

            try
            {
                Usuario uBuscado = db.usuarios.Find(u.cedula);
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
