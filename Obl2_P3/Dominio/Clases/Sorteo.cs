using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Repositorios;
using System.Data.Entity;
using Dominio.Contexto_DB;

namespace Dominio.Clases
{
    [Table("Sorteo")]
    public class Sorteo
    {
        #region Props

        [Key]
        [ForeignKey("Vivienda")]
        public int SorteoId { get; set; }

        [Required]
        public virtual Vivienda Vivienda { get; set; }


        [Required]
        public DateTime fecha { get; set; }

        public virtual ICollection<Postulante> Postulantes { get; set; }

        [ForeignKey("Ganador")]
        public int GanadorId { get; set; }
        public virtual Postulante Ganador { get; set; }

        public bool realizado { get; set; } = false;

        #endregion

        #region Metodos

        //public Postulante sortear()
        public bool sortear()
        {
            try
            {
                Random r = new Random();
                //Lista de postulantes ordenada alfabeticamente
                List<Postulante> pAux = this.Postulantes.OrderBy(px => px.apellido).ToList();

                //Bandera, en true para que entre al loop
                bool reSortear = true;
                Postulante ganador = null;
                while (reSortear)
                {

                    //en la primer pasada lo desactivamos ya que si el ganador no es adjudicatario el loop se corta
                    reSortear = false;
                    //Rango del random [ 0 - Count-1] para abarcar todo el indice de la lista.
                    ganador = pAux[r.Next(Postulantes.Count - 1)];

                    //si llegase a ser adjudicatario, vuelve a entrar al loop y a reasignar otro ganador random
                    if (ganador.adjudicatario)
                    {
                        reSortear = true;
                    }
                }

                RepoPostulante rp = new RepoPostulante();
                Postulante p = rp.findByCi(ganador.UsuarioId.ToString());
                if (p != null)
                {
                    if(rp.winnerAssignSorteo(p.UsuarioId, this))
                    {
                        RepoSorteo rs = new RepoSorteo();

                        if (rs.assignGanador(p, this.SorteoId)) return true;
                        
                        else return false;
                    }
                    return false;
                }
                else return false;

            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }

        public bool esValido()
        {
            return
                this.fecha != null && (fecha.Day >= DateTime.Now.Day && fecha.Year >= DateTime.Now.Year && fecha.Month >= DateTime.Now.Month) &&
                this.Vivienda != null;
        }

        public bool esValidoParaSortear()
        {
            return
                this.fecha != null && fecha.ToShortDateString() == DateTime.Now.ToShortDateString() &&
                this.Vivienda != null &&
                this.Postulantes.Count > 0 &&
                this.Ganador == null;
        }

        #endregion
    }
}
