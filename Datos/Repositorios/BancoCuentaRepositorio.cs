using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class BancoCuentaRepositorio : RepositorioBase<BancoCuenta>
    {

        private SAC_Entities context;

        public BancoCuentaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public List<BancoCuenta> GetAllCuenta()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.BancoCuenta.ToList();

        }

        public BancoCuenta GetCuentaPorId(int id)
        {
            return context.BancoCuenta.Where(p => p.Id == id).First();           
        }


        public BancoCuenta GetBancoCuentaPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.BancoCuenta.Where(p => p.Id == id ).First();
        }

        public List<BancoCuenta> GetBancoPorNombre(string strBanco)
        {
            List<BancoCuenta> p = (from c in context.BancoCuenta
                                   where c.Activo == true && c.Banco.Nombre.Contains(strBanco)
                                   select c).ToList();
            return p;
        }
        public List<BancoCuenta> GetBancoCuentaPorNombre(string strBanco)
        {
            List<BancoCuenta> p = (from c in context.BancoCuenta
                                   where c.Activo == true && c.BancoDescripcion.Contains(strBanco)
                                   select c).ToList();
            return p;
        }
        public Banco GetBancoPorId(int id)
        {
            return context.Banco.Where(p => p.Id == id).First();
        }

        public Banco GetBancoPorIdLazy(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Banco.Where(p => p.Id == id).First();
        }


        public List<BancoCuentaBancaria> GetmMovimientosPendientesCuentaBancaria(int idBanco, DateTime fecha)

        {
            DateTime date = Convert.ToDateTime(fecha).Date.AddDays(1);
            context.Configuration.LazyLoadingEnabled = false;
            List<BancoCuentaBancaria> listaCliente = context.BancoCuentaBancaria
                                                    .Include("GrupoCaja") // armar la relacion
                                                    .Where(p => p.Activo == true
                                                   && p.NumeroCierre == 0
                                                   && p.IdBancoCuenta == idBanco
                                                   && p.Fecha < date)
                                                  .OrderBy(p => p.FechaEfectiva).ToList();
            return listaCliente;

        }

        public List<BancoCuenta> GetBancoPorFecha(int idBanco, DateTime fecha)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<BancoCuenta> listaCliente = context.BancoCuenta
             .Include("Banco")
             .Include("BancoCuentaBancaria")
             .Include("Imputacion")
           .Where(p => p.Activo == true && p.NumeroCierre == 0 && p.IdBanco == idBanco).ToList(); //  && p.Fecha <= Fecha).ToList();


            return listaCliente;

        }

        public BancoCuenta CierreDeCuentaBancaria(int id, decimal saldoCierre, DateTime Fecha)
        {
            BancoCuenta bancoCuenta = GetBancoCuentaPorId(id);
            bancoCuenta.Saldo = saldoCierre;
            bancoCuenta.NumeroCierre += 1;
            bancoCuenta.Fecha = Fecha;
            context.SaveChanges();
            return bancoCuenta;
        }

        public BancoCuenta GuardarCuentaBancaria(BancoCuenta model)
        {
            context.Configuration.LazyLoadingEnabled = false;
            context.BancoCuenta.Add(model);
            context.SaveChanges();
            return model;//  Insertar(bancoCuenta);
        }

        public List<Banco> GetAllBanco()
        {
            return context.Banco.Where(p => p.Activo == true).ToList();
        }

        public BancoCuenta UpdateCuentaBancaria(BancoCuenta model)
        {
            BancoCuenta bancoCuenta = GetBancoCuentaPorId(model.Id);
            bancoCuenta.BancoDescripcion = model.BancoDescripcion;
            bancoCuenta.Codigo = model.Codigo;
            bancoCuenta.IdBanco = model.IdBanco;
            bancoCuenta.IdImputacion = model.IdImputacion;
            bancoCuenta.IdMoneda = model.IdMoneda;
            bancoCuenta.Activo = model.Activo;
            bancoCuenta.IdUsuario = model.IdUsuario;
            bancoCuenta.UltimaModificacion = model.UltimaModificacion;
            context.SaveChanges();
            return bancoCuenta;
        }

        public BancoCuenta DeshabilitarCuentaBancaria(BancoCuenta model)
        {
            BancoCuenta bancoCuenta = GetBancoCuentaPorId(model.Id);           
            bancoCuenta.Activo = model.Activo;
            bancoCuenta.IdUsuario = model.IdUsuario;
            bancoCuenta.UltimaModificacion = model.UltimaModificacion;
            context.SaveChanges();
            return bancoCuenta;
        }
    }
}
