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
        public DbSet<Barrio> barrio { get; set; }
        public DbSet<Vivienda> vivienda { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Parametro> parametro { get; set; }
        public DbSet<Sorteo> sorteo { get; set; }

        public Contexto():base("cadenaConexion")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Sorteo>()
            .HasKey(t => t.id)
            ;

        }

    }
}
