using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Interfaces
{
    interface IRepoUsuario
    {
        bool add(Usuario u);
        bool update(Usuario u);
        bool delete(Usuario u);
        IEnumerable<Usuario> findAll();
        Usuario findByCi(string uCi);
        bool validarLogin(Usuario u);
    }
}
