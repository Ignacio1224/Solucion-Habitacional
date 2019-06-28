using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dominio.Clases;
using Dominio.Repositorios;

namespace Obl2_P3.Models
{
    public class VMUsuario
    {
        [DisplayName("Cedula:")]
        public string cedula { get; set; }
        [DisplayName("Usuario:")]
        public string clave { get; set; }

        public bool validarModel()
        {
            if (Utilidades.isNumber(cedula) && Utilidades.esCampoValido(cedula) && Utilidades.esCampoValido(clave)) return true;

            else return false;
        }

        public static VMUsuario ConvertToVMUsuario(Usuario user)
        {
            return new VMUsuario()
            {
                cedula = user.cedula,
                clave = user.clave
            };
        }

        public static Usuario ConvertToUsuario(VMUsuario user)
        {
            if (!user.validarModel()) return null;

            return new Usuario()
            {
                cedula = user.cedula,
                clave = user.clave
            };
        }
    }
}