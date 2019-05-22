using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Interfaces
{
    public interface IRepoPostulante
    {
        bool add(Postulante p);
        bool update(Postulante p);
        bool delete(Postulante p);
        Usuario findByCi(int pCi);
        IEnumerable<Postulante> findAll();
        bool login(Postulante u);

        
    }
}
