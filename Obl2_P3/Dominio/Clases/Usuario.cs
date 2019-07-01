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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(9)]
        public string cedula { get; set; }

        //Expresión regular de contraseña.
        //La contraseña debe tener al menos 6 caracteres, no más de 15, y debe incluir al menos 
        //una letra mayúscula, una letra minúscula y un dígito numérico.
        [Required]
        [RegularExpression(@"^ (? =. * \ d) (? =. * [az]) (? =. * [AZ]). {6,15} $")]
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
