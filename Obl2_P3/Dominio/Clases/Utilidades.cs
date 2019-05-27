using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public static class Utilidades
    {
        #region Metodos

        public static bool esCampoValido(string param)
        {
            return param != "";
        }

        public static bool esCampoValido(string param, int minLength, int maxLength)
        {
            return esCampoValido(param) && param.Length > minLength && param.Length < maxLength;
        }

        public static bool esEmailValido(string param)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(param);
                return addr.Address == param;
            }
            catch
            {
                return false;
            }
        }


        // TODO: Sin implementar
        public static decimal convertirValor(Parametro moneda, Decimal valor)
        {
            return 0;
        }
        #endregion
    }
}
