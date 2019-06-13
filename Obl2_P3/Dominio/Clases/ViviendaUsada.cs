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
        public decimal monto_contribucion { get; set; }

        #endregion

        #region Metodos

        public override bool esValida()
        {
            return 
                base.esValida() &&
                monto_contribucion > 0;  
        }

        public override string ToString()
        {
            return base.ToString() + "#" + monto_contribucion;
        }

        #endregion

    }
}
