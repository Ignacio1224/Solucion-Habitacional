using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Dominio.Interfaces;
using Dominio.Contexto_DB;

namespace Dominio.Repo
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
            catch (Exception ex)
            {
                string msj = ex.Message;
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
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }

        public IEnumerable<Vivienda> findAll()
        {
            try
            {
                if (db.vivienda.Count() > 0)
                {
                    var vLista = from Vivienda v in db.vivienda select v;
                    db.Dispose();

                    return vLista.ToList();
                }
                else return null;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return null;
            }
        }

        public Vivienda findById(int vId)
        {
            Vivienda v = db.vivienda.Find(vId);
            if (v != null) return v;
            else return null;
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

                    if (vBuscada.validar())
                    {
                        db.SaveChanges();
                        db.Dispose();
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
            }
        }
    }
}
