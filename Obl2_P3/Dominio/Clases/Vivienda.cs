using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public abstract class Vivienda
    {
        #region Props
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Calle { get; set; }
        public int NroPuerta { get; set; }
        public string Descripcion { get; set; }
        public Barrio Barrio { get; set; }
        public byte Banios { get; set; }
        public byte Dormitorios { get; set; }
        public short Metraje { get; set; }
        public short AnioConstruccion { get; set; }
        public Parametro Moneda { get; set; }
        public decimal PrecioFinal { get; set; }
        #endregion

        #region Metodos
        public virtual bool Validar()
        {
            if (!Utilidades.CampoVacio(this.Calle) && !Utilidades.EsNumerico(this.Calle))
            {
                if (this.NroPuerta > 0 && this.NroPuerta < 10000)
                {
                    if (!Utilidades.CampoVacio(this.Descripcion) && !Utilidades.EsNumerico(this.Descripcion))
                    {
                        if (this.Banios > 0)
                        {
                            if (this.Dormitorios > 0)
                            {
                                if (this.PrecioFinal > 0)
                                {
                                    if (this.Metraje > 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
