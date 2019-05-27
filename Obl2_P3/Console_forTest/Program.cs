using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Repositorios;

namespace Console_forTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Barrio barrioX = new Barrio() { nombre = "Cerrito", descripcion = "Casas sobre un cerrito" };

            RepoBarrio repoB = new RepoBarrio();
            if (repoB.add(barrioX))
            {
                Console.WriteLine("Sabelo");
                Console.ReadKey();
            }else
            {
                Console.WriteLine("No saberlo");
                Console.ReadKey();
            }
        }
    }
}
