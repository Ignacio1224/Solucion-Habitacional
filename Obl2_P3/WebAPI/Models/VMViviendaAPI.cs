using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio.Clases;

namespace WebAPI.Models
{
    public class VMViviendaAPI
    {
        #region props
        public int ViviendaId { get; set; }
        public Estaditos estado { get; set; }
        public enum Estaditos
        {
            Habilitada,
            Inhabilitada,
            Recibida,
            Sorteada
        };
        public string calle { get; set; }
        public int nro_puerta { get; set; }
        public string descripcion { get; set; }
        public string Barrio { get; set; }
        public int cant_banio { get; set; }
        public int cant_dormitorio { get; set; }
        public decimal metraje { get; set; }
        public int anio_construccion { get; set; }
        public string moneda { get; set; }
        public decimal precio_final { get; set; }
        public Enum estados;
        public string tipo_vivienda { get; set; }
        public decimal monto_contribucion { get; set; }
        #endregion

        #region methods
        public static List<VMViviendaAPI> ConvertToVMViviendaAPI(IEnumerable<Vivienda> vs)
        {
            List<VMViviendaAPI> vmvs = new List<VMViviendaAPI>();

            if (vs != null)
            {
                foreach (Vivienda v in vs)
                {
                    VMViviendaAPI a = ConvertToVMViviendaAPI(v);
                    vmvs.Add(a);
                }
            }
            return vmvs;
        }

        public static VMViviendaAPI ConvertToVMViviendaAPI(Vivienda v)
        {
            return new VMViviendaAPI
            {
                ViviendaId = v.ViviendaId,
                estado = (VMViviendaAPI.Estaditos)v.estado,
                calle = v.calle,
                nro_puerta = v.nro_puerta,
                descripcion = v.descripcion,
                Barrio = v.Barrio.nombre_barrio,
                cant_banio = v.cant_banio,
                cant_dormitorio = v.cant_dormitorio,
                metraje = v.metraje,
                anio_construccion = v.anio_construccion,
                moneda = v.moneda,
                precio_final = v.precio_final,
                monto_contribucion = v.ReturnContribucion(),
                tipo_vivienda = v.ReturnType()
            };
        }
        #endregion
    }
}