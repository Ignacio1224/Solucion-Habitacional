using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("Sorteo")]
    public class Sorteo
    {
        #region Props
        [ForeignKey("Vivienda")]
        public int SorteoId { get; set; }

        [Required]
        public DateTime fecha { get; set; }

        [Required]
        public virtual Vivienda Vivienda { get; set; }

        public virtual ICollection<Postulante> Postulantes { get; set; }

        public virtual Postulante Ganador { get; set; }

        public bool realizado { get; set; } = false;
        
        #endregion

        #region Metodos

        public Postulante sortear()
        {
            Random r = new Random();
            this.Ganador = ((List<Postulante>)Postulantes)[r.Next(Postulantes.Count)];
            return this.Ganador;  
        }

        public bool esValido()
        {
            return 
                fecha != null &&
                Vivienda != null &&
                Postulantes != null;
        }

        #endregion
    }
}
