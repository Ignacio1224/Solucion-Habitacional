using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Interfaces
{
    interface IRepoVivienda
    {
        bool Add(Vivienda v);
        bool Update(Vivienda v);
        bool Delete(Vivienda v);
        Vivienda FindById(int vId);
        IEnumerable<Vivienda> FindAll();
    }
}
