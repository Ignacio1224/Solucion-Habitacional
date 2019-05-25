using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    public class ViviendaUsada:Vivienda
    {
        #region Props
        [Required]
        public decimal MontoContribucion { get; set; }
        #endregion

        #region Metodos
        public override bool validar()
        {
            if (base.validar())
            {
                if (this.anioConstruccion < (DateTime.Now.Year - 2))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}
