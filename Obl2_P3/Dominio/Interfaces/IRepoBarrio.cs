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
        bool add(Barrio b);
        bool update(Barrio b);
        bool delete(Barrio b);
        IEnumerable<Barrio> findAll();
        Barrio findByName(string bName);
        Barrio findById(int id);
    }
}
