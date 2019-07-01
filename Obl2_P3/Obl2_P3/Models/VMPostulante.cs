using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//
using Dominio.Clases;
using Dominio.Repositorios;
using WebAPI.Models;

namespace Obl2_P3.Models
{
    public class VMPostulante
    {
        #region props

        [DisplayName("Nombre:")]
        public string nombre { get; set; }

        [DisplayName("Apellido:")]
        public string apellido { get; set; }

        [DisplayName("Email:")]
        public string email { get; set; }

        [DisplayName("Fecha de nacimiento:")]
        public DateTime fecha_nac { get; set; }

        [DisplayName("Cédula de identidad:")]
        public string cedula { get; set; }

        [DisplayName("Contraseña:")]
        [PasswordPropertyTextAttribute(true)]
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

        public static Postulante ConvertToPostulante(VMPostulante p)
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


        public static VMPostulanteAPI ConvertToVMPostulanteAPI(VMPostulante p)
        {
            return new VMPostulanteAPI
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