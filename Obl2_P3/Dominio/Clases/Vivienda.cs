﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("Vivienda")]
    public abstract class Vivienda
    {
        #region Props

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ViviendaId { get; set; }

        [Required]
        public Estados estado { get; set; }

        public enum Estados
        {
            Habilitada,
            Inhabilitada,
            Recibida,
            Sorteada
        };

        [Required]
        public string calle { get; set; }

        [Required]
        public int nro_puerta { get; set; }

        [Required]
        public string descripcion { get; set; }

        [Required]
        [ForeignKey("Barrio")]
        public int BarrioId { get; set; }
        public virtual Barrio Barrio { get; set; }

        [Required]
        public int cant_banio { get; set; }

        [Required]
        public int cant_dormitorio { get; set; }

        [Required]
        public decimal metraje { get; set; }

        [Required]
        public int anio_construccion { get; set; }
        
        public String moneda { get; set; } // Parametro --> se busca por nombre

        [Required]
        public decimal precio_final { get; set; }

        public virtual Sorteo Sorteo { get; set; }

        #endregion

        #region Metodos

        public virtual bool esValida()
        {
            return 
                Utilidades.esCampoValido(this.calle) &&
                this.nro_puerta > 0 && this.nro_puerta < 9999 &&
                Utilidades.esCampoValido(this.descripcion) &&
                this.cant_banio > 0 &&
                this.cant_dormitorio > 0 &&
                this.precio_final > 0 &&
                this.metraje > 0;
        }

        public override string ToString()
        {
            return calle + "#" + nro_puerta + "#" + descripcion + "#"
                + cant_banio + "#" + cant_dormitorio + "#" + metraje
                 + "#" + anio_construccion + "#" + precio_final;
        }

        public abstract string ReturnType();

        public abstract decimal ReturnContribucion();
        #endregion
    }
}
