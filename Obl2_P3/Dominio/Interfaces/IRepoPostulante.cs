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
        Postulante findByCi(string pCi);
        IEnumerable<Postulante> findAll();
        bool validarLogin(Postulante p);
        bool winnerAssignSorteo(int pId, Sorteo sorteo);


    }
}
