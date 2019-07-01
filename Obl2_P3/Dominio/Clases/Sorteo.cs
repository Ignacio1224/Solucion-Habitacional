using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Repositorios;
using Dominio.Contexto_DB;
using System.Data.Entity;

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
                List<Postulante> pAux = this.Postulantes.OrderBy(px => px.apellido).ToList();
                Postulante ganador = new Postulante();
                
                bool reSortear = true;
                while (reSortear){
                    reSortear = false;

                    ganador = pAux[r.Next(Postulantes.Count - 1)];

                    if (ganador.adjudicatario)
                    {
                        reSortear = true;
                    }
                }                

                Contexto db = new Contexto();

                Postulante p = db.postulantes.Where(x => x.UsuarioId == ganador.UsuarioId ).FirstOrDefault();

                p.adjudicatario = true;
                p.Sorteo = this;
                this.realizado = true;

                db.Entry(p.Sorteo).State = EntityState.Modified;
                db.Entry(p.Sorteo.Vivienda.Barrio).State = EntityState.Unchanged;
                db.Entry(p).State = EntityState.Modified;
                db.Entry(p.email).State = EntityState.Unchanged;
                db.Entry(this).State = EntityState.Modified;

                db.SaveChanges();
                db.Dispose();

                return true;
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
