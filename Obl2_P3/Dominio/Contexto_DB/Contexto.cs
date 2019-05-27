using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dominio.Clases;

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
    }
}
