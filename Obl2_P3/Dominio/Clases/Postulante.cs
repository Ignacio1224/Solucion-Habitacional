using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    public class Postulante : Usuario
    {
        #region Props

        [Key]
        public int id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string nombre { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string apellido { get; set; }

        [Required]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string email { get; set; }

        [Required]
        public DateTime fechaNac { get; set; }

        #endregion

        #region Metodos

        public bool es_mayor()
        {
            return this.fechaNac.AddYears(18) <= DateTime.Today;
        }

        public override bool esValido()
        {
            return
                base.esValido() &&
                Utilidades.esCampoValido(this.nombre, 2, 50) &&
                Utilidades.esCampoValido(this.apellido, 2, 50) &&
                Utilidades.esEmailValido(this.email) &&
                es_mayor();
        }

        public override string getRole()
        {
            return "postulante";
        }

        #endregion
    }
}
