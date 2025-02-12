using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
  public class ChequeRepositorio : RepositorioBase<Cheque>
    {
        private SAC_Entities context;

        public ChequeRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<Cheque> GetAllCheque()
        {
            List<Cheque> listaCheque = context.Cheque.Where(p => p.Activo == true && p.NumeroPago == null && p.IdFactura == null).ToList();
            //List<Cheque> listaCheque = context.Cheque.ToList();
            listaCheque = listaCheque.OrderBy(p => p.NumeroCheque).ToList();

           // List<Cheque> listaCheque = context.Cheque.Where(p => p.Activo == true && p.Endosado == false ).ToList();
           // return listaCheque.OrderBy(p => p.NumeroCheque).ToList();
            return listaCheque;
        }


        public Cheque obtenerCheque (int idCheque)
        {
            return context.Cheque.Where(p => p.Id == idCheque).First();
        }


        public Cheque Agregar(Cheque oCheque)
        {
            return Insertar(oCheque);
        }

        public Cheque Actualizar (Cheque oCheque)
        {
            Cheque nCheque = obtenerCheque(oCheque.Id);
            nCheque.Id = oCheque.Id;
            nCheque.NumeroCheque = oCheque.NumeroCheque;
            nCheque.IdBanco = oCheque.IdBanco;
            nCheque.Fecha = oCheque.Fecha;
            nCheque.DiaClearing = oCheque.DiaClearing;
            nCheque.Importe = oCheque.Importe;
            nCheque.IdCliente = oCheque.IdCliente;
            nCheque.Descripcion = oCheque.Descripcion;
            nCheque.NumeroRecibo = oCheque.NumeroRecibo;
            nCheque.FechaIngreso = oCheque.FechaIngreso;
            nCheque.FechaEgreso = oCheque.FechaEgreso;
            nCheque.Destino = oCheque.Destino;
            nCheque.IdMoneda = oCheque.IdMoneda;
            nCheque.IdGrupoCaja = oCheque.IdGrupoCaja;
            nCheque.IdFactura = oCheque.IdFactura;
            nCheque.NumeroPago = oCheque.NumeroPago;
            nCheque.Registro = oCheque.Registro;
            nCheque.Proveedor = oCheque.Proveedor;
            nCheque.Endosado = oCheque.Endosado;
            nCheque.Activo = oCheque.Activo;
            nCheque.IdUsuario = oCheque.IdUsuario;
            nCheque.UltimaModificacion = oCheque.UltimaModificacion;
            context.SaveChanges();
            return nCheque;
        }


        public List<Cheque> obtenerChequePorBanco(int cIdbanco, DateTime cfechadesde, DateTime cfechahasta)
        {

            List<Cheque> listaCheque = context.Cheque
                                        .Include("BancoCuenta")
                                        .Where(p => p.Activo == true && p.IdBanco == cIdbanco && p.Fecha >= cfechadesde && p.Fecha <= cfechahasta)
                                        .OrderBy(p => p.NumeroCheque)
                                        .ToList();

            return listaCheque;

        }

        public List<Cheque> BuscarCheque(int idCliente, int idbanco, DateTime fechaDesde, DateTime fechaHasta)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Cheque> listaCheque = context.Cheque
                                        .Include(b => b.BancoCuenta)
                                        .Include(b => b.BancoCuenta.Banco)
                                        .Where(p => p.Activo == true                                      
                                        & (idbanco == 0 | p.IdBanco == idbanco)
                                        & (idCliente == 0 | p.IdCliente == idCliente)
                                        & p.Fecha >= fechaDesde
                                        && p.Fecha <= fechaHasta)
                                        .OrderBy(p => p.NumeroCheque)
                                        .ToList();

            return listaCheque;
        }

       
        public Cheque ExisteCheque(Cheque cheque)
        {
            return context.Cheque.Where(p => p.Activo == true
            && p.IdCliente == cheque.IdCliente
            && p.IdBanco == cheque.IdBanco
            && p.NumeroCheque == cheque.NumeroCheque).FirstOrDefault();
        }

        public List<Cheque> obtenerChequePorCliente(int cIdCliente, DateTime cfechadesde, DateTime cfechahasta)
        {

            List<Cheque> listaCheque = context.Cheque.Where(p => p.Activo == true && p.IdCliente == cIdCliente && p.Fecha >= cfechadesde && p.Fecha <= cfechahasta).ToList();

            listaCheque = listaCheque.OrderBy(p => p.NumeroCheque).ToList();


            return listaCheque;


        }

     }
}
