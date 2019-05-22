using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Sorteo
    {
        #region Props

        public int id { get; set; }
        public DateTime fecha { get; set; }
        public Vivienda vivienda { get; set; }
        public IEnumerable<Postulante> postulantes { get; set; }
        public Postulante ganador { get; set; }
        public bool realizado { get; set; }
        
        #endregion

        #region Metodos

        //sin implementar
        public bool Validar()
        {
            return false;
        }
        public int RealizarSorteo()
        {
            return - 1;
        }

        #endregion
    }
}
