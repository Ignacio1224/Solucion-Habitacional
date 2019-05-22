using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio.Clases
{
    public class Barrio
    {
        #region Props
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Metodos
        public bool Validar()
        {
            return Utilidades.IsNaN(this.Nombre) && !Utilidades.CampoVacio(this.Nombre);
        }
        #endregion
    }
}
