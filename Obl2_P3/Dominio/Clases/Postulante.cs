using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("Postulante")]
    public class Postulante : Usuario
    {
        #region Props

        //[Key]
        //public int PostulanteID { get; set; }

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
        [StringLength(254)]
        public string email { get; set; }

        [Required]
        public DateTime fecha_nac { get; set; }

        [Required]
        public bool adjudicatario { get; set; } = false;

        //[ForeignKey("Sorteo")]
        //public int SorteoId { get; set; }
        public virtual Sorteo SorteoGanado { get; set; } // Sorteo ganado

        public virtual ICollection<Sorteo> Sorteos { get; set; }

        #endregion

        #region Metodos

        public override bool esValido()
        {
            return
                base.esValido() &&
                Utilidades.esCampoValido(this.nombre, 2, 50) &&
                Utilidades.esCampoValido(this.apellido, 2, 50) &&
                Utilidades.esEmailValido(this.email) && 
                esMayorDeEdad();
        }

        public bool esMayorDeEdad()
        {
            int age = DateTime.Now.Year - this.fecha_nac.Year;
            if (DateTime.Now.Month < this.fecha_nac.Month || 
                (DateTime.Now.Month == this.fecha_nac.Month && DateTime.Now.Day < this.fecha_nac.Day))
                age--;
            return age > 18;
        }

        public override string getRole()
        {
            return "postulante";
        }

        #endregion
    }
}
