using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Usuario
    {
        #region Props

        public int cedula { get; set; }
        public string clave { get; set; }
        
        #endregion

        #region Metodos

       public bool Validar()
        {
            
            return this.cedula > 0 && this.cedula < 100000000 && !Utilidades.campoVacio(this.clave);
        }

        public virtual string devolverTipo() {
            return "jefe";
        }

        #endregion
    }
}
