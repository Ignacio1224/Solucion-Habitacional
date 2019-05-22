using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Postulante : Usuario
    {
        #region Metodos
        public override string DevolverTipo()
        {
            return "Postulante";
        }
        #endregion

    }
}
