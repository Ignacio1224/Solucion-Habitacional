using System;
using System.Collections.Generic;
using System.IO;
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

        public static void escribirErrores(List<String> errores)
        {
            using (StreamWriter file = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Errores.txt"))
            {
                foreach (string s in errores)
                {
                    file.WriteLineAsync(s);
                }

                file.Close();
            }
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

        public static bool isNumber(string campo)
        {
            try
            {
                Convert.ToInt32(campo);
                return true;
            }catch
            {
                return false;
            }
        }

        public static decimal convertirValor(Parametro moneda, Decimal valor)
        {
            return 0;
        }

        #endregion
    }
}
