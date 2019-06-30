using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Repositorios;

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

                while (reSortear){

                    //en la primer pasada lo desactivamos ya que si el ganador no es adjudicatario el loop se corta
                    reSortear = false;
                    //Rango del random [ 0 - Count-1] para abarcar todo el indice de la lista.
                    this.Ganador = pAux[r.Next(Postulantes.Count - 1)];

                    //si llegase a ser adjudicatario, vuelve a entrar al loop y a reasignar otro ganador random
                    if (this.Ganador.adjudicatario)
                    {
                        reSortear = true;
                    }
                }                

                RepoPostulante rp = new RepoPostulante();
                Postulante p = rp.findByCi(this.Ganador.UsuarioId.ToString());
                p.adjudicatario = true;

                rp.update(p);

                this.realizado = true;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool esValido()
        {
            return
                this.fecha != null && fecha.ToShortDateString() == DateTime.Now.ToShortDateString() &&
                this.Vivienda != null;
        }

        #endregion
    }
}
