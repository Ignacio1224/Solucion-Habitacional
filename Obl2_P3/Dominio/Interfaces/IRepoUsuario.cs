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
        bool Add(Usuario u);
        bool Update(Usuario u);
        bool Delete(Usuario u);
        Usuario FindByCi(int uCi);
        IEnumerable<Usuario> FindAll();
    }
}
