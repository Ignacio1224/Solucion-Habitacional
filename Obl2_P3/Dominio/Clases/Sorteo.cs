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

        [Key]
        [ForeignKey("Vivienda")]
        public int SorteoId { get; set; }

        [Required]
        public virtual Vivienda Vivienda { get; set; }


        [Required]
        public DateTime fecha { get; set; }

        public virtual ICollection<Postulante> Postulantes { get; set; }

        [ForeignKey("Ganador")]
        public int GanadorId { get; set; }
        public virtual Postulante Ganador { get; set; }

        public bool realizado { get; set; } = false;

        #endregion

        #region Metodos

        //public Postulante sortear()
        public void sortear()
        {
            Random r = new Random();
            //Lista de postulantes ordenada alfabeticamente
            List<Postulante> pAux = this.Postulantes.OrderBy(p => p.apellido).ToList();

            //Rango del random [ 0 - Count-1] para abarcar todo el indice de la lista.
            this.Ganador = pAux[r.Next(Postulantes.Count - 1)];
            this.realizado = true;
        }

        public bool esValido()
        {
            return
                this.fecha != null && fecha.ToShortDateString() == DateTime.Now.ToShortDateString() &&
                this.Vivienda != null;
        }

        #endregion
    }
}
