using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dominio.Clases;

namespace Dominio.Contexto_DB
{
    class Contexto:DbContext
    {
        DbSet<Postulante> postulantesContext { get; set; }
        DbSet<Barrio> barrioContext { get; set; }
        DbSet<Vivienda> viviendaContext { get; set; }
        DbSet<Usuario> usuarioContext { get; set; }
        DbSet<Parametro> parametroContext { get; set; }
        DbSet<Sorteo> sorteoContext { get; set; }
    }
}
