using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;

namespace Datos.Repositorios
{
    public class BancoCuentaBancariaRepositorio : RepositorioBase<BancoCuentaBancaria>
    {
        private SAC_Entities context;

        public BancoCuentaBancariaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<BancoCuentaBancaria> GetAllBancoCuentaBancaria()
        {
            return context.BancoCuentaBancaria.ToList();
        }


        public BancoCuentaBancaria Agregar(BancoCuentaBancaria oBancoCuentaBancaria)
        {
            
            return Insertar(oBancoCuentaBancaria);
        }

        public List<BancoCuentaBancaria> GetAllMovimientosConciliados(int id)
        {
            return context.BancoCuentaBancaria
                .Where(i => i.Conciliacion == true
                            && i.Activo == true
                            && i.IdBancoCuenta == id)
                            .OrderBy(b => b.FechaEfectiva).ToList();
        }

        public void UpdateNumeroCierreMovimiento(BancoCuentaBancaria model)
        {
            BancoCuentaBancaria bancoCuenta = GetBancoCuentaBancariaPorId(model.Id);
            bancoCuenta.Activo = true;
            bancoCuenta.UltimaModificacion = DateTime.Now;
            bancoCuenta.NumeroCierre = model.NumeroCierre;
            context.SaveChanges();
        }

        private BancoCuentaBancaria GetBancoCuentaBancariaPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.BancoCuentaBancaria
                           .Where(i => i.Activo == true && i.Id == id).FirstOrDefault();
        }

        public void ConciliarMovimiento(int item)
        {
            BancoCuentaBancaria bancoCuenta = GetBancoCuentaBancariaPorId(item);
            bancoCuenta.Activo = true;
            bancoCuenta.UltimaModificacion = DateTime.Now;
            if (bancoCuenta.Conciliacion) {
                bancoCuenta.Conciliacion = false;
            }
            else
            {
                bancoCuenta.Conciliacion = true;
            }
            context.SaveChanges();
        }

        // Impresion Cta Bria
        public List<BancoCuentaBancaria> GetCtaBriaImpresion(int id, int idBanco)
        {
           
           return context.BancoCuentaBancaria
                    .Where(i => i.Activo == true
                                && i.NumeroCierre == id
                                && i.IdBancoCuenta == idBanco
                                && i.Conciliacion == true)
                    .OrderBy(b => b.FechaEfectiva).ToList();
           

        }
        // Impresion Cta Bria
        public List<BancoCuentaBancaria> GetCtaBriaImpresion0(int id, int idBanco,DateTime fecha)
        {
            return context.BancoCuentaBancaria
                   .Where(i => i.Activo == true
                               && i.NumeroCierre == id
                               && i.IdBancoCuenta == idBanco
                               && i.FechaIngreso <= fecha)
                   .OrderBy(b => b.FechaEfectiva).ToList();


        }
    }
}