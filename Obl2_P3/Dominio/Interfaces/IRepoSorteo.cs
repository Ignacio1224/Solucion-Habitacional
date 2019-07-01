using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Interfaces
{
    interface IRepoSorteo
    {
        bool add(Sorteo s);
        bool delete(Sorteo s);
        IEnumerable<Sorteo> findAll();
        Sorteo findById(int sId);
        Sorteo raffle(Sorteo s);
        bool update(Sorteo s);
        bool inscribePostulanteAtSorteo(Postulante p, int sId);
        bool assignGanador(Postulante p, int sId);
    }
}
