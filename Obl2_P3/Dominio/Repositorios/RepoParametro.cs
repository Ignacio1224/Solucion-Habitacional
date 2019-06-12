using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Interfaces;
using Dominio.Contexto_DB;
using System.IO;
using System.Diagnostics;

namespace Dominio.Repositorios
{
    public class RepoParametro : IRepoParametro, IRepoImport
    {

        public bool add(Parametro p)
        {
            Contexto db = new Contexto();

            if (p == null) return false;

            try
            {
                db.parametros.Add(p);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool delete(Parametro p)
        {
            Contexto db = new Contexto();

            if (p == null) return false;

            try
            {
                db.parametros.Remove(p);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Parametro findByName(string pName)
        {
            Contexto db = new Contexto();

            Parametro p = null;

            try
            { 
                p = db.parametros.Find(pName);
                db.Dispose();
            }
            catch (Exception)
            {
                return null;
            }

            return p;
        }

        public bool import()
        {
            Contexto db = new Contexto();

            List<Parametro> parametros_a_importar = new List<Parametro>();

            bool imported = false;

            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Parametros.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {

                    string[] s = ln.Split('#');
                    parametros_a_importar.Add(new Parametro
                    {
                        nombre_parametro = s[0],
                        valor = Convert.ToDecimal(s[1])
                    });
                }

                file.Close();
            }

            try
            {
                db.parametros.AddRange(parametros_a_importar);
                db.SaveChanges();

                imported = true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return imported;
        }

        public bool update(Parametro p)
        {
            Contexto db = new Contexto();

            try
            {
                Parametro pBuscado = db.parametros.Find(p.nombre_parametro);
                if (pBuscado != null)
                {
                    pBuscado.valor = p.valor;
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
