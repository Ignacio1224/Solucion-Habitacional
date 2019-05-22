using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Interfaces
{
    interface IRepoBarrio
    {
        bool Add(Barrio b);
        bool Update(Barrio b);
        bool Delete(Barrio b);
        Barrio FindByName(string bName);
        IEnumerable<Barrio> FindAll();
    }
}
