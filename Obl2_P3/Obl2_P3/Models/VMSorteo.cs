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
    public class VMSorteo
    {
        [DisplayName("Sorteo Id")]
        public int SorteoId { get; set; }

        [DisplayName("Fecha")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime fecha { get; set; }

        [DisplayName("Vivienda sorteada")]
        [Required]
        public int Vivienda { get; set; }

        [DisplayName("Postuantes")]
        public ICollection<Postulante> Postulantes { get; set; }

        [DisplayName("Ganador")]
        public Postulante Ganador { get; set; }

        [DisplayName("Realizado")]
        public bool realizado { get; set; } = false;

        public int BarrioId { get; set; }


        public static VMSorteo ConvertToVMSorteo (Sorteo s)
        {
            return new VMSorteo
            {
                SorteoId = s.SorteoId,
                fecha = s.fecha,
                Vivienda = s.Vivienda.ViviendaId,
                Postulantes = s.Postulantes,
                Ganador = s.Ganador,
                realizado = s.realizado
            };
        }

        public static List<VMSorteo> ConvertToVMSorteo(IEnumerable<Sorteo> ss)
        {
            List<VMSorteo> lista_sorteos = new List<VMSorteo>();

            if (ss != null)
            {
                foreach (Sorteo s in ss)
                {
                    lista_sorteos.Add(ConvertToVMSorteo(s));
                }
            }

            return lista_sorteos;
        }

        public static Sorteo ConvertToSorteo (VMSorteo vms)
        {
            RepoVivienda rv = new RepoVivienda();
            return new Sorteo
            {
                SorteoId = vms.SorteoId,
                fecha = vms.fecha,
                Vivienda = rv.findById(vms.Vivienda),
                Postulantes = vms.Postulantes,
                Ganador = vms.Ganador,
                realizado = vms.realizado
            };
        }

    }
}