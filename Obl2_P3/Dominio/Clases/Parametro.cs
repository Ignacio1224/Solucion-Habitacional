using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("Parametro")]
    public class Parametro
    {
        #region Props

        [Key]
        public string nombreParametro { get; set; }

        [Required]
        public decimal valor { get; set; }

        #endregion


        #region Metodos

        public bool esValido()
        {
            return
                Utilidades.esCampoValido(this.nombreParametro, 1, 255);
        }

        #endregion

    }
}
