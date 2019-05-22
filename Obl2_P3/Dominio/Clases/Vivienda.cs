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
        public int id { get; set; }
        public string estado { get; set; }
        public string calle { get; set; }
        public int nroPuerta { get; set; }
        public string descripcion { get; set; }
        public Barrio barrio { get; set; }
        public byte banios { get; set; }
        public byte dormitorios { get; set; }
        public short metraje { get; set; }
        public short anioConstruccion { get; set; }
        public Parametro moneda { get; set; }
        public decimal precioFinal { get; set; }
        #endregion

        #region Metodos
        public virtual bool Validar()
        {
            if (!Utilidades.campoVacio(this.calle) && !Utilidades.esNumerico(this.calle))
            {
                if (this.nroPuerta > 0 && this.nroPuerta < 10000)
                {
                    if (!Utilidades.campoVacio(this.descripcion) && !Utilidades.esNumerico(this.descripcion))
                    {
                        if (this.banios > 0)
                        {
                            if (this.dormitorios > 0)
                            {
                                if (this.precioFinal > 0)
                                {
                                    if (this.metraje > 0)
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
