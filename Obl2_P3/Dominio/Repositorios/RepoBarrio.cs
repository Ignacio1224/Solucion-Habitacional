using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Contexto_DB;
using Dominio.Interfaces;
using System.IO;

namespace Dominio.Repositorios
{
    public class RepoBarrio : IRepoBarrio
    {
        Contexto db = new Contexto();

        public bool add(Barrio b)
        {
            if (b == null) return false;

            try
            {
                db.barrio.Add(b);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return false;
            }

        }

        public bool delete(Barrio b)
        {
            if (b == null) return false;

            try
            {
                db.barrio.Remove(b);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Barrio> findAll()
        {
            List<Barrio> listaBarrios = null;

            if (db.barrio.Count() > 0)
            {
                listaBarrios = (from Barrio b in db.barrio select b).ToList();
                db.Dispose();
            }

            return listaBarrios;
        }

        public Barrio findByName(string bName)
        {
            try
            {
                Barrio barrioBuscado = db.barrio.Find(bName);
                db.Dispose();
                return barrioBuscado;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public bool import()
        {
            bool imported = false;

            using (StreamReader file = new StreamReader("../../../Archivos_Para_Importar/Barrios.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {

                    string[] s = ln.Split('#');

                    add(new Barrio
                    {
                        nombreBarrio = s[0],
                        descripcion = s[1]
                    });

                }

                file.Close();
                imported = true;
            }


            return imported;
        }

        public bool update(Barrio b)
        {
            try
            { 
                Barrio barrioBuscado = db.barrio.Find(b.nombreBarrio);
                if (barrioBuscado != null)
                {
                    barrioBuscado.descripcion = b.descripcion;
                    db.SaveChanges();
                    db.Dispose();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

