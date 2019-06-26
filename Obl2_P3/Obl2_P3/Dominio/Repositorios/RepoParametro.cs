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

        public IEnumerable<Parametro> findAll()
        {
            Contexto db = new Contexto();

            List<Parametro> listaParametros = null;

            if (db.parametros.Count() > 0)
            {
                listaParametros = (from Parametro p in db.parametros select p).ToList();
                db.Dispose();
            }

            return listaParametros;
        }

        public Parametro findByName(string pName)
        {
            Contexto db = new Contexto();

            Parametro p = null;

            try
            { 
                p = db.parametros.Where(pa => pa.nombre_parametro == pName).SingleOrDefault();
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
            List<string> errores = new List<string>();

            bool imported = true;

            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Parametros.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {

                    string[] paramet = ln.Split('#');
                    foreach (String st in paramet)
                    {
                        string[] s = st.Split('=');

                        parametros_a_importar.Add(new Parametro
                        {
                            nombre_parametro = s[0],
                            valor = Convert.ToDecimal(s[1])
                        });
                    }
                }

                file.Close();
            }

            try
            {

                foreach (Parametro p in parametros_a_importar) {
                    if (! p.esValido()) {
                        errores.Add("Nombre o valor no válido#" + p.ToString());
                        imported = false;
                    } else {
                        if (findByName(p.nombre_parametro) != null)
                        {
                            errores.Add("Parámetro duplicado#" + p.ToString());
                            imported = false;
                        }
                        else
                        {
                            db.parametros.Add(p);
                        }
                    }
                }
                
                db.SaveChanges();

                Utilidades.escribirErrores(errores);

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
