using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("ViviendaUsada")]
    public class ViviendaUsada:Vivienda
    {
        #region Props

        [Required]
        public decimal montoContribucion { get; set; }

        #endregion

        #region Metodos

        public override bool esValida()
        {
            return 
                base.esValida() &&
                montoContribucion > 0;  
        }

        #endregion

    }
}
