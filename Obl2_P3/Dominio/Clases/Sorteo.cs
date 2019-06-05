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
        public int id { get; set; }

        [Required]
        public DateTime fecha { get; set; }

        public Vivienda vivienda { get; set; }

        public List<Postulante> postulantes { get; set; }

        public Postulante ganador { get; set; }

        public bool realizado { get; set; } = false;
        
        #endregion

        #region Metodos

        public Postulante sortear()
        {
            Random r = new Random();
            this.ganador = this.postulantes[r.Next(this.postulantes.Count)];
            return this.ganador;  
        }

        public bool esValido()
        {
            return 
                fecha != null &&
                vivienda != null &&
                postulantes != null;
        }

        #endregion
    }
}
