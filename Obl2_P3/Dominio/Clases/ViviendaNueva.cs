using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class ViviendaNueva:Vivienda
    {
        public override bool Validar()
        {
            if (base.Validar())
            {
                if (this.AnioConstruccion >= (DateTime.Now.Year - 2) && this.AnioConstruccion <= (DateTime.Now.Year))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

