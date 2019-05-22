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
        bool Add(Sorteo s);
        bool Update(Sorteo s);
        bool Delete(Sorteo s);
        Sorteo FindById(int sId);
        IEnumerable<Sorteo> FindAll();
    }
}
