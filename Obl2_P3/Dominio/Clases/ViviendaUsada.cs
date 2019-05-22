using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class ViviendaUsada:Vivienda
    {
        public decimal MontoContribucion { get; set; }

        public override bool Validar()
        {
            if (base.Validar())
            {
                if (this.AnioConstruccion < (DateTime.Now.Year - 2))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
