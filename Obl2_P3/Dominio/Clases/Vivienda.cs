using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    //Dejando esta sentencia EF mapearia la tabla Vivienda como una 3er tabla

    //[Table("Vivienda")]   

    //Colocandole esta misma sentencia a las otras dos tablas de vivienda especificas mapeamos correctamente la herencia
    //EF termina generando solo 2 tablas, una para Usada  y otra para Nueva.
    [Table("Vivienda")]
    public abstract class Vivienda
    {
        #region Props

        [Key]
        public int id { get; set; }

        [Required]
        public string estado { get; set; }

        [Required]
        public string calle { get; set; }

        [Required]
        public int nroPuerta { get; set; }

        [Required]
        public string descripcion { get; set; }

        [ForeignKey("barrio")]
        [Required]
        [Index("IDX_ViviendaBarrio")]
        public Barrio barrio { get; set; }

        [Required]
        public int banios { get; set; }

        [Required]
        public int dormitorios { get; set; }

        [Required]
        public decimal metraje { get; set; }

        [Required]
        public int anioConstruccion { get; set; }

        [ForeignKey("moneda")]
        [Required,Index("IDX_MonedaVivienda")]
        public Parametro moneda { get; set; }

        [Required]
        public decimal precioFinal { get; set; }

        #endregion

        #region Metodos

        public virtual bool esValida()
        {
            return 
                (estado == "Recibida" || estado == "Habilitada" || estado == "Inhabilitada") &&
                Utilidades.esCampoValido(this.calle) &&
                this.nroPuerta > 0 && this.nroPuerta < 9999 &&
                Utilidades.esCampoValido(this.descripcion) &&
                this.banios > 0 &&
                this.dormitorios > 0 &&
                this.precioFinal > 0 &&
                this.metraje > 0;
        }
        
        #endregion
    }
}
