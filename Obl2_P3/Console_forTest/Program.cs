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
            Barrio barrioX = new Barrio() { nombreBarrio = "Cerrito", descripcion = "Casas sobre un cerrito" };

            RepoBarrio repoB = new RepoBarrio();

            try
            {
                repoB.add(barrioX);
                Console.WriteLine("Gracias marcelo");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            
           
        }
    }
}
