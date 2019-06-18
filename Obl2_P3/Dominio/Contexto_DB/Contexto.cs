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

        public Contexto() : base("cadenaConexion")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // No pluraliza el nombre de las tablas
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Barrio>()
                .HasMany<Vivienda>(b => b.Vivienda)
                .WithRequired(v => v.Barrio)
                .WillCascadeOnDelete();

        }

    }
}

