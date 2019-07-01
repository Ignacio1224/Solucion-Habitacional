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
        bool add(Parametro p);
        bool update(Parametro p);
        bool delete(Parametro p);
        IEnumerable<Parametro> findAll();
        Parametro findByName(string pName);
    }
}
