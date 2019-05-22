using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public static class Utilidades
    {
        public static bool EsNumerico(string param)
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
        public static bool CampoVacio(string param)
        {
            if (param == "")
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}
