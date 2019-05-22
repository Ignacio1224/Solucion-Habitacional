using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Sorteo:IEquatable<Sorteo>
    {
        #region Props
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Vivienda Vivienda { get; set; }
        public IEnumerable<Usuario> Postulantes { get; set; }
        public string Estado { get; set; }
        #endregion

        #region Metodos
        public bool Equals(Sorteo s)
        {
            return this.Vivienda == s.Vivienda;
        }

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
