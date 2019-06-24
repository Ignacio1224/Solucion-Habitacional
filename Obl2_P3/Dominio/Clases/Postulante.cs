﻿using System;
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

        //[ForeignKey("Sorteo")]
        //public int SorteoId { get; set; }
        public virtual Sorteo Sorteo { get; set; } // Sorteo ganado

        public virtual ICollection<Sorteo> Sorteos { get; set; }

        #endregion

        #region Metodos

        public bool es_mayor()
        {
            return this.fecha_nac.AddYears(18) <= DateTime.Today;
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
