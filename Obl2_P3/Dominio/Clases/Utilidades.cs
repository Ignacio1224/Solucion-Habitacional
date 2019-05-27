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
        public static bool esNumerico(string param)
        {
            try
            {
                Convert.ToInt32(param);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool campoVacio(string param)
        {
            return param == "";
        }

        /*
        public static decimal convertirValor(Parametro moneda, Decimal valor)
        {

        }*/
        #endregion
    }
}
