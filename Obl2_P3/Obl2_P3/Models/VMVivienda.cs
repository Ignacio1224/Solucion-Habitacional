using Dominio.Clases;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Obl2_P3.Models
{
    public class VMVivienda
    {
        public int ViviendaId { get; set; }

        [DisplayName("Estado")]
        public Vivienda.Estados estado { get; set; }

        [DisplayName("Calle")]
        public string calle { get; set; }

        [DisplayName("Nº de puerta")]
        public int nro_puerta { get; set; }

        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        [DisplayName("Barrio")]
        public string Barrio { get; set; }

        [DisplayName("Cantidad de baños")]
        public int cant_banio { get; set; }

        [DisplayName("Cantidad de dormitorios")]
        public int cant_dormitorio { get; set; }

        [DisplayName("Superficie")]
        public decimal metraje { get; set; }

        [DisplayName("Año de construcción")]
        public int anio_construccion { get; set; }

        [DisplayName("Moneda")]
        public String moneda { get; set; }

        [DisplayName("Precio final")]
        public decimal precio_final { get; set; }

        [DisplayName("Sorteo")]
        public virtual Sorteo Sorteo { get; set; }

        public Enum estados;


        public static List<VMVivienda> ConvertToVMVivienda (IEnumerable<Vivienda> vs)
        {
            List<VMVivienda> vmvs = new List<VMVivienda>();

            if (vs != null)
            {
                foreach (Vivienda v in vs)
                {
                    VMVivienda a = ConvertToVMVivienda(v);
                    vmvs.Add(a);
                }
            }
            return vmvs;
        }

        public static VMVivienda ConvertToVMVivienda (Vivienda v)
        {
            return new VMVivienda
            {
                ViviendaId = v.ViviendaId,
                estado = v.estado,
                calle = v.calle,
                nro_puerta = v.nro_puerta,
                descripcion = v.descripcion,
                Barrio = v.Barrio.nombre_barrio,
                cant_banio = v.cant_banio,
                cant_dormitorio = v.cant_dormitorio,
                metraje = v.metraje,
                anio_construccion = v.anio_construccion,
                moneda = v.moneda,
                precio_final = v.precio_final
            };
        }

    }
}