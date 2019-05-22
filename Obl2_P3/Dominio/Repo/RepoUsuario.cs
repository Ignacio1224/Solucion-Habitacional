using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Interfaces;

namespace Dominio.Repo
{
    public class RepoUsuario : IRepoUsuario
    {
        public bool add(Usuario u)
        {
            throw new NotImplementedException();
        }

        public bool delete(Usuario u)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> findAll()
        {
            throw new NotImplementedException();
        }

        public Usuario findByCi(int uCi)
        {
            throw new NotImplementedException();
        }

        public bool login(Usuario u)
        {
            throw new NotImplementedException();
        }

        public bool update(Usuario u)
        {
            throw new NotImplementedException();
        }
    }
}
