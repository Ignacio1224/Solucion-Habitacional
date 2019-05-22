using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Postulante : Usuario
    {

        #region

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public DateTime fechaNac { get; set; }

        #endregion

        #region Metodos
        public override string devolverTipo()
        {
            return "postulante";
        }

        
        public bool es_mayor ()
        {
            return DateTime.Now.Year - this.fechaNac.Year > 18;
        }
        #endregion

    }
}
