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
    public class RepoVivienda : IRepoVivienda, IRepoImport
    {
        Contexto db = new Contexto();

        public bool add(Vivienda v)
        {
            if (v == null) return false;

            try
            {
                db.viviendas.Add(v);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool delete(Vivienda v)
        {
            if (v == null) return false;

            try
            {
                db.viviendas.Remove(v);
                db.SaveChanges();
                db.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Vivienda> findAll()
        {
            List<Vivienda> vLista = null;
            try
            {
                if (db.viviendas.Count() > 0)
                {
                    vLista = (from Vivienda v in db.viviendas select v).ToList();
                    db.Dispose();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return vLista;

        }

        public Vivienda findById(int vId)
        {
            return db.viviendas.Find(vId);
        }

        public bool import()
        {
            Contexto db = new Contexto();

            RepoBarrio rb = new RepoBarrio();
            RepoParametro rp = new RepoParametro();

            List<Vivienda> viviendas_a_importar = new List<Vivienda>();
            List<string> errores = new List<string>();

            bool imported = true;

            Parametro p = null;

            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Viviendas.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {

                    string[] s = ln.Split('#');

                    int id = Convert.ToInt32(s[0]);
                    string calle = s[1];
                    int nroPuerta = Convert.ToInt32(s[2]);
                    Barrio b = rb.findByName(s[3]);
                    string descripcion = s[4];
                    int nroBanios = Convert.ToInt32(s[5]);
                    int nroDormitorios = Convert.ToInt32(s[6]);
                    decimal metraje = Convert.ToDecimal(s[7]);
                    int anio = Convert.ToInt32(s[8]);
                    decimal precio = Convert.ToDecimal(s[9]);
                    string tipo = s[10];
                    string estado = "Habilitada";

                    if (tipo == "Nueva")
                    {
                        viviendas_a_importar.Add(new ViviendaNueva
                        {
                            ViviendaId = id,
                            calle = calle,
                            nro_puerta = nroPuerta,
                            Barrio = b,
                            descripcion = descripcion,
                            cant_banio = nroBanios,
                            cant_dormitorio = nroDormitorios,
                            metraje = metraje,
                            anio_construccion = anio,
                            precio_final = precio,
                            estado = estado,
                            Parametro = rp.findByName("cotizacionUI")
                        });
                    }
                    else if (tipo == "Usada")
                    {
                        viviendas_a_importar.Add(new ViviendaUsada
                        {
                            ViviendaId = id,
                            calle = calle,
                            nro_puerta = nroPuerta,
                            Barrio = b,
                            descripcion = descripcion,
                            cant_banio = nroBanios,
                            cant_dormitorio = nroDormitorios,
                            metraje = metraje,
                            anio_construccion = anio,
                            precio_final = precio,
                            estado = estado,
                            Parametro = rp.findByName("cotizacionUSD"),
                            monto_contribucion = Convert.ToDecimal(s[11])
                        });
                    }


                }

                file.Close();
            }

            try
            {
                foreach (Vivienda v in viviendas_a_importar)
                {
                    if (!v.esValida())
                    {
                        errores.Add("Vivienda no válida#" + v.ToString());
                        imported = false;
                    }
                    else
                    {
                        db.viviendas.Add(v);
                        db.Entry(v.Barrio).State = System.Data.Entity.EntityState.Unchanged;
                        db.Entry(v.Parametro).State = System.Data.Entity.EntityState.Unchanged;
                        db.SaveChanges();
                    }
                }



                Utilidades.escribirErrores(errores);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return imported;
        }

        public bool update(Vivienda v)
        {

            if (v == null) return false;

            try
            {
                Vivienda vBuscada = db.viviendas.Find(v.ViviendaId);
                if (vBuscada != null)
                {
                    vBuscada.anio_construccion = v.anio_construccion;
                    vBuscada.cant_banio = v.cant_banio;
                    vBuscada.Barrio = v.Barrio;
                    vBuscada.calle = v.calle;
                    vBuscada.descripcion = v.descripcion;
                    vBuscada.cant_dormitorio = v.cant_dormitorio;
                    vBuscada.estado = v.estado;
                    vBuscada.metraje = v.metraje;
                    vBuscada.Parametro = v.Parametro;
                    vBuscada.nro_puerta = v.nro_puerta;
                    vBuscada.precio_final = v.precio_final;

                    if (vBuscada.esValida())
                    {
                        db.SaveChanges();
                        db.Dispose();
                        return true;
                    }
                    return false;
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
