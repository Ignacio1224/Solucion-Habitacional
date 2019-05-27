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
    public class RepoVivienda : IRepoVivienda
    {
        Contexto db = new Contexto();

        public bool add(Vivienda v)
        {
            if (v == null) return false;

            try
            {
                db.vivienda.Add(v);
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
                db.vivienda.Remove(v);
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
                if (db.vivienda.Count() > 0)
                {
                    vLista = (from Vivienda v in db.vivienda select v).ToList();
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
            return db.vivienda.Find(vId);
        }

        public bool import()
        {
            bool imported = false;
            RepoBarrio rb = new RepoBarrio();
            RepoParametro rp = new RepoParametro();

            using (StreamReader file = new StreamReader("../../../Archivos_Para_Importar/Viviendas.txt"))
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
                        add(new ViviendaNueva
                        {
                            id = id,
                            calle = calle,
                            nroPuerta = nroPuerta,
                            barrio = b,
                            descripcion = descripcion,
                            banios = nroBanios,
                            dormitorios = nroDormitorios,
                            metraje = metraje,
                            anioConstruccion = anio,
                            precioFinal = precio,
                            estado = estado,
                            moneda = rp.findByName("cotizacionUI")
                        });
                    }
                    else if (tipo == "Usada")
                    {
                        add(new ViviendaUsada
                        {
                            id = id,
                            calle = calle,
                            nroPuerta = nroPuerta,
                            barrio = b,
                            descripcion = descripcion,
                            banios = nroBanios,
                            dormitorios = nroDormitorios,
                            metraje = metraje,
                            anioConstruccion = anio,
                            precioFinal = precio,
                            estado = estado,
                            moneda = rp.findByName("cotizacionUSD"),
                            montoContribucion = Convert.ToDecimal(s[11])
                        });
                    }


                }

                file.Close();
                imported = true;
            }


            return imported;
        }

        public bool update(Vivienda v)
        {

            if (v == null) return false;

            try
            {
                Vivienda vBuscada = db.vivienda.Find(v.id);
                if (vBuscada != null)
                {
                    vBuscada.anioConstruccion = v.anioConstruccion;
                    vBuscada.banios = v.banios;
                    vBuscada.barrio = v.barrio;
                    vBuscada.calle = v.calle;
                    vBuscada.descripcion = v.descripcion;
                    vBuscada.dormitorios = v.dormitorios;
                    vBuscada.estado = v.estado;
                    vBuscada.metraje = v.metraje;
                    vBuscada.moneda = v.moneda;
                    vBuscada.nroPuerta = v.nroPuerta;
                    vBuscada.precioFinal = v.precioFinal;

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
