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
        bool add(Vivienda v);
        bool update(Vivienda v);
        bool delete(Vivienda v);
        Vivienda findById(int vId);
        IEnumerable<Vivienda> findAll();
        IEnumerable<Vivienda> findByBarrio(int bId);
        IEnumerable<Vivienda> findByBarrioToRaffle(int bId);
        IEnumerable<Vivienda> getByPriceRange(decimal pMin, decimal pMax);
        IEnumerable<Vivienda> getByBarrio(int idBarrio);
        IEnumerable<Vivienda> getByState(int state);
        IEnumerable<Vivienda> getByType(string type);



    }
}
