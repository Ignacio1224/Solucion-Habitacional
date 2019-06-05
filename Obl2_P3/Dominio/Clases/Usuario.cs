using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{

    [Table("Usuario")]
    public class Usuario
    {
        #region Props

        [Key]
        [MinLength(7)]
        [MaxLength(9)]
        public string cedula { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6, 255}$")]
        public string clave { get; set; }

        #endregion

        #region Metodos

        public virtual bool esValido()
        {
            return 
                Utilidades.esCampoValido(this.cedula, 7, 9) &&
                Utilidades.esCampoValido(this.clave, 6, 255);
        }

        public virtual string getRole()
        {
            return "jefe";
        }

        #endregion
    }
}
