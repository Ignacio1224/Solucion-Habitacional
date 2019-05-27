using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Interfaces;
using Dominio.Contexto_DB;
using System.IO;

namespace Dominio.Repositorios
{
    public class RepoParametro : IRepoParametro
    {
        Contexto db = new Contexto();

        public bool add(Parametro p)
        {
            if (p == null) return false;

            try
            {
                db.parametro.Add(p);
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
            if (p == null) return false;

            try
            {
                db.parametro.Remove(p);
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
            Parametro p = null;

            try
            { 
                p = db.parametro.Find(pName);
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
            bool imported = false;

            using (StreamReader file = new StreamReader("../../../Archivos_Para_Importar/Parametros.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {

                    string[] s = ln.Split('#');

                    add(new Parametro
                    {
                        nombre = s[0],
                        valor = Convert.ToDecimal(s[1])
                    });

                }

                file.Close();
                imported = true;
            }


            return imported;
        }

        public bool update(Parametro p)
        {
            try
            {
                Parametro pBuscado = db.parametro.Find(p.nombre);
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
