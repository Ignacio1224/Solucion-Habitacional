using Dominio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class VMPostulanteAPI
    {
        #region props
        public string nombre { get; set; }

        public string apellido { get; set; }

        public string email { get; set; }

        public DateTime fecha_nac { get; set; }

        public string cedula { get; set; }

        public string clave { get; set; }
        #endregion

        #region methods
        public bool esValido()
        {
            return
                Utilidades.esCampoValido(this.cedula, 7, 9) &&
                Utilidades.esCampoValido(this.clave, 6, 255) &&
                Utilidades.esCampoValido(this.nombre, 2, 50) &&
                Utilidades.esCampoValido(this.apellido, 2, 50) &&
                Utilidades.esEmailValido(this.email) &&
                fecha_nac.AddYears(18) <= DateTime.Today;
        }

        public static Postulante ConvertToPostulante(VMPostulanteAPI p)
        {
            return new Postulante
            {
                apellido = p.apellido,
                cedula = p.cedula,
                clave = p.clave,
                email = p.email,
                fecha_nac = p.fecha_nac,
                nombre = p.nombre
            };
        }
        #endregion
    }
}