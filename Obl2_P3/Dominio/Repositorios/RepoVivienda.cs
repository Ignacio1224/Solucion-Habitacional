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
                            id_vivienda = id,
                            calle = calle,
                            nro_puerta = nroPuerta,
                            barrio = b,
                            descripcion = descripcion,
                            cant_banio = nroBanios,
                            cant_dormitorio = nroDormitorios,
                            metraje = metraje,
                            anio_construccion = anio,
                            precio_final = precio,
                            estado = estado,
                            moneda = rp.findByName("cotizacionUI")
                        });
                    }
                    else if (tipo == "Usada")
                    {
                        add(new ViviendaUsada
                        {
                            id_vivienda = id,
                            calle = calle,
                            nro_puerta = nroPuerta,
                            barrio = b,
                            descripcion = descripcion,
                            cant_banio = nroBanios,
                            cant_dormitorio = nroDormitorios,
                            metraje = metraje,
                            anio_construccion = anio,
                            precio_final = precio,
                            estado = estado,
                            moneda = rp.findByName("cotizacionUSD"),
                            monto_contribucion = Convert.ToDecimal(s[11])
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
                Vivienda vBuscada = db.viviendas.Find(v.id_vivienda);
                if (vBuscada != null)
                {
                    vBuscada.anio_construccion = v.anio_construccion;
                    vBuscada.cant_banio = v.cant_banio;
                    vBuscada.barrio = v.barrio;
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
