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
using System.Data.Entity;

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
            catch (Exception ex)
            {
                string error = ex.Message;
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
                    vLista = (from Vivienda v in db.viviendas select v).Include(v => v.Barrio).Include(v => v.Sorteo).ToList();
                    db.Dispose();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return vLista;

        }

        public IEnumerable<int> findAllEnabled()
        {
            List<int> vLista = null;
            try
            {
                if (db.viviendas.Count() > 0)
                {
                    vLista = db.viviendas.Where(v => v.estado == Vivienda.Estados.Habilitada).Select(v => v.ViviendaId).ToList();
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
            //Contexto db = new Contexto();
            
            Vivienda v = db.viviendas.Where(vi => vi.ViviendaId == vId).Include(vi => vi.Barrio).SingleOrDefault();

            db.Dispose();

            return v;
        }

        public bool import()
        {
            Contexto db = new Contexto();

            RepoBarrio rb = new RepoBarrio();
            RepoParametro rp = new RepoParametro();

            List<Vivienda> viviendas_a_importar = new List<Vivienda>();
            List<string> errores = new List<string>();

            bool imported = true;

            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Archivos\\Viviendas.txt"))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {

                    string[] s = ln.Split('#');

                    int id = Convert.ToInt32(s[0]);
                    string calle = s[1];
                    int nroPuerta = Convert.ToInt32(s[2]);
                    int b = rb.findByName(s[3]).BarrioId;
                    string descripcion = s[4];
                    int nroBanios = Convert.ToInt32(s[5]);
                    int nroDormitorios = Convert.ToInt32(s[6]);
                    decimal metraje = Convert.ToDecimal(s[7]);
                    int anio = Convert.ToInt32(s[8]);
                    decimal precio = Convert.ToDecimal(s[9]);
                    string tipo = s[10];

                    if (tipo == "Nueva")
                    {
                        viviendas_a_importar.Add(new ViviendaNueva
                        {
                            ViviendaId = id,
                            calle = calle,
                            nro_puerta = nroPuerta,
                            BarrioId = b,
                            descripcion = descripcion,
                            cant_banio = nroBanios,
                            cant_dormitorio = nroDormitorios,
                            metraje = metraje,
                            anio_construccion = anio,
                            precio_final = precio,
                            estado = Vivienda.Estados.Recibida,
                            moneda = "cotizacionUI"
                        });
                    }
                    else if (tipo == "Usada")
                    {
                        viviendas_a_importar.Add(new ViviendaUsada
                        {
                            ViviendaId = id,
                            calle = calle,
                            nro_puerta = nroPuerta,
                            BarrioId = b,
                            descripcion = descripcion,
                            cant_banio = nroBanios,
                            cant_dormitorio = nroDormitorios,
                            metraje = metraje,
                            anio_construccion = anio,
                            precio_final = precio,
                            estado = Vivienda.Estados.Recibida,
                            moneda = "cotizacionUSD",
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
                        RepoVivienda rv = new RepoVivienda();
                        if (rv.findById(v.ViviendaId) != null)
                        {
                            errores.Add("Vivienda duplicada#" + v.ToString());
                            imported = false;
                        }
                        else
                        {

                            db.viviendas.Add(v);
                            db.SaveChanges();
                        }
                    }
                }


                Utilidades.escribirErrores(errores);

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                imported = false;
            }

            return imported;
        }

        public bool update(Vivienda v)
        {
            bool updated = false;

            if (v == null) return updated;

            RepoBarrio rb = new RepoBarrio();

            try
            {
                Vivienda vBuscada = db.viviendas.Find(v.ViviendaId);
                if (vBuscada != null)
                {
                    vBuscada.anio_construccion = v.anio_construccion;
                    vBuscada.cant_banio = v.cant_banio;
                    vBuscada.BarrioId = v.Barrio.BarrioId;
                    vBuscada.calle = v.calle;
                    vBuscada.descripcion = v.descripcion;
                    vBuscada.cant_dormitorio = v.cant_dormitorio;
                    vBuscada.estado = v.estado;
                    vBuscada.metraje = v.metraje;
                    vBuscada.moneda = v.moneda;
                    vBuscada.nro_puerta = v.nro_puerta;
                    vBuscada.precio_final = v.precio_final;

                    if (vBuscada.esValida())
                    {
                        db.SaveChanges();
                        db.Dispose();
                        updated = true;
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return updated;
            }
            return updated;
        }

    }
}
