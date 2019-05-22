using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Interfaces;

namespace Dominio.Repo
{
    public class RepoPostulante : IRepoPostulante
    {
        public bool add(Postulante p)
        {
            throw new NotImplementedException();
        }

        public bool delete(Postulante p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Postulante> findAll()
        {
            throw new NotImplementedException();
        }

        public Usuario findByCi(int pCi)
        {
            throw new NotImplementedException();
        }

        public bool login(Postulante u)
        {
            throw new NotImplementedException();
        }

        public bool update(Postulante p)
        {
            throw new NotImplementedException();
        }
    }
}
