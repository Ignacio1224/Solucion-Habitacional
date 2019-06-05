using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dominio.Clases;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Dominio.Contexto_DB
{
    internal class Contexto : DbContext
    {
        public DbSet<Postulante> postulantes { get; set; }
        public DbSet<Barrio> barrios { get; set; }
        public DbSet<Vivienda> viviendas { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Parametro> parametros { get; set; }
        public DbSet<Sorteo> sorteos { get; set; }

        public Contexto():base("cadenaConexion")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Sorteo 1 --> 1 Vivienda
            modelBuilder.Entity<Vivienda>()
                .HasKey(v => v.id_vivienda);

            modelBuilder.Entity<Sorteo>()
                .HasRequired(s => s.vivienda)
                .WithRequiredPrincipal(s => s.sorteo);


            // Sorteo * <--> * Postulante
            modelBuilder.Entity<Sorteo>()
                .HasMany(s => s.postulantes)
                .WithMany(p => p.sorteos);


            // Sorteo 1 --> 1 Postulante
            modelBuilder.Entity<Postulante>()
                .HasKey(p => p.id_postulante);

            modelBuilder.Entity<Sorteo>()
                .HasRequired(s => s.ganador)
                .WithRequiredPrincipal(s => s.sorteo);


            // Vivienda * --> 1 Parametro
            modelBuilder.Entity<Parametro>()
                .HasMany(s => s.viviendas)
                .WithRequired(s => s.moneda);


            // Vivienda * --> 1 Barrio
            modelBuilder.Entity<Barrio>()
                .HasMany(b => b.viviendas)
                .WithRequired(v => v.barrio);

        }

    }
}
