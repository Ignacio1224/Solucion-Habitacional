using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class ViviendaNueva:Vivienda
    {
        #region Metodos
        public override bool validar()
        {
            if (base.validar())
            {
                if (this.anioConstruccion >= (DateTime.Now.Year - 2) && this.anioConstruccion <= (DateTime.Now.Year))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}

