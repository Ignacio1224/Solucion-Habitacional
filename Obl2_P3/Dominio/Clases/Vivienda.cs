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
    //Colocandole esta misma sentencia a las otras dos tablas de vivienda especificas mapeamos correctamente la herencia
    //EF termina generando solo 2 tablas, una para Usada  y otra para Nueva.

    //[Table("Vivienda")]   
    public abstract class Vivienda
    {
        #region Props

        [Key]
        public int id { get; set; }

        [Required]
        public bool estado { get; set; }

        [Required]
        public string calle { get; set; }

        [Required]
        public int nroPuerta { get; set; }

        [Required]
        public string descripcion { get; set; }

        [ForeignKey("ViviendaBarrio")]
        [Required]
        [Index("IDX_ViviendaBarrio")]
        public Barrio barrio { get; set; }

        [Required]
        public byte banios { get; set; }

        [Required]
        public byte dormitorios { get; set; }

        [Required]
        public short metraje { get; set; }

        [Required]
        public short anioConstruccion { get; set; }

        [ForeignKey("MonedaVivienda"),Required,Index("IDX_MonedaVivienda")]
        public Parametro moneda { get; set; }

        [Required]
        public decimal precioFinal { get; set; }

        #endregion

        #region Metodos
        public virtual bool validar()
        {
            if (!Utilidades.campoVacio(this.calle) && !Utilidades.esNumerico(this.calle))
            {
                if (this.nroPuerta > 0 && this.nroPuerta < 10000)
                {
                    if (!Utilidades.campoVacio(this.descripcion) && !Utilidades.esNumerico(this.descripcion))
                    {
                        if (this.banios > 0)
                        {
                            if (this.dormitorios > 0)
                            {
                                if (this.precioFinal > 0)
                                {
                                    if (this.metraje > 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
