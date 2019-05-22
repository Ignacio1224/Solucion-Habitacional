using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public abstract class Usuario : IEquatable<Usuario>
    {
        #region Props
        public int Cedula { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaNac { get; set; }
        #endregion

        #region Metodos

        public bool Equals(Usuario u)
        {
            return this.Cedula == u.Cedula;
        }

        public bool Validar()
        {
            if (this.Cedula > 0 && this.Cedula < 100000000)
            {
                if (!Utilidades.CampoVacio(this.Clave) && !Utilidades.CampoVacio(this.Nombre) && !Utilidades.CampoVacio(this.Apellido) && !Utilidades.CampoVacio(this.Email))
                {
                    if (!Utilidades.EsNumerico(this.Nombre) && !Utilidades.EsNumerico(this.Apellido) && !Utilidades.EsNumerico(this.Email))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract string DevolverTipo();

        #endregion
    }
}
