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
        public int cedula { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6, 255}$")]
        public string clave { get; set; }
        
        #endregion

        #region Metodos

       public bool Validar()
        {   
            return this.cedula > 0 && this.cedula < 100000000 && !Utilidades.campoVacio(this.clave);
        }

        public virtual string devolverTipo() {
            return "jefe";
        }

        #endregion
    }
}
