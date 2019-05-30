using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("Barrio")]
    public class Barrio
    {
        #region Props
       
        [Key]
        public string nombreBarrio { get; set; }

        [Required]
        public string descripcion { get; set; }
       
        #endregion


        #region Metodos
        
        public bool esValido()
        {
            return 
                Utilidades.esCampoValido(this.nombreBarrio, 1, 255) &&
                Utilidades.esCampoValido(this.nombreBarrio, 1, 255);
        }

        #endregion

    }
}
