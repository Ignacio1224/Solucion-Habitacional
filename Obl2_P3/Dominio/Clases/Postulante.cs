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

        #region

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
        public override string devolverTipo()
        {
            return "postulante";
        }


        public bool es_mayor()
        {
            return DateTime.Now.Year - this.fechaNac.Year > 18;
        }
        #endregion

    }
}
