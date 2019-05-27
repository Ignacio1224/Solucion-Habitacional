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

        [ForeignKey("ViviendaSorteo"),Required]
        public Vivienda vivienda { get; set; }

        [ForeignKey("PostulantesSorteo")]
        public List<Postulante> postulantes { get; set; }

        [ForeignKey("GanadorSorteo")]
        public Postulante ganador { get; set; }

        [Required]
        public bool realizado { get; set; } = false;
        
        #endregion

        #region Metodos

        //sin implementar
        public bool validar()
        {
            return false;
        }
        public int realizarSorteo()
        {
            return - 1;
        }

        #endregion
    }
}
