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
            bool imported = false;

            using (StreamReader file = new StreamReader("../../../Archivos_Para_Importar/Parametros.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {

                    string[] s = ln.Split('#');

                    add(new Parametro
                    {
                        nombre_parametro = s[0],
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
