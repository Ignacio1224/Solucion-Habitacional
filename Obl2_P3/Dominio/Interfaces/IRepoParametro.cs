using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Interfaces
{
    interface IRepoParametro
    {
        bool Add(Parametro p);
        bool Update(Parametro p);
        bool Delete(Parametro p);
        Parametro FindByName(string pName);
    }
}
