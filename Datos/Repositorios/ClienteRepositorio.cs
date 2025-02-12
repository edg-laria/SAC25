using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace Datos.Repositorios
{
  public class ClienteRepositorio : RepositorioBase<Cliente>
    {
        private SAC_Entities context;

        public Func<FactVenta, object> Fecha { get; private set; }

        public ClienteRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

         #region "Metodos de Actualizacion"

        /// <summary>
        ///  Agregar Cliente
        /// </summary>

        public Cliente Agregar(Cliente oCliente)

        {

            try
            {

                return Insertar(oCliente);
            }


            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

            }



            catch (Exception ex)
            {
               
                        System.Diagnostics.Debug.WriteLine("Property: " + ex.InnerException + " Error: " + ex.Message);
               

            }

            return Insertar(oCliente);

        }

        /// <summary>
        ///  Actualizar Cliente
        /// </summary>
        public Cliente ActualizarCliente(Cliente oCliente)
        {
            Cliente nCliente = GetClientePorId (oCliente.Id);
            nCliente.Id = oCliente.Id;
            nCliente.Codigo = oCliente.Codigo;
            nCliente.Nombre = oCliente.Nombre;
            nCliente.IdTipoiva = oCliente.IdTipoiva;
            nCliente.Cuit = oCliente.Cuit;
            nCliente.DiasFactura = oCliente.DiasFactura;
            nCliente.IdImputacion = oCliente.IdImputacion;
            nCliente.Observaciones = oCliente.Observaciones;
            nCliente.Email = oCliente.Email;
            nCliente.IdPieNota = oCliente.IdPieNota;
            nCliente.IdIdioma = oCliente.IdIdioma;
            nCliente.IdTipoCliente = oCliente.IdTipoCliente;
            nCliente.Visible = oCliente.Visible;
            nCliente.IdNotaPieB = oCliente.IdNotaPieB;        
            nCliente.IdTipoMoneda = oCliente.IdTipoMoneda;
            nCliente.IdGrupoPresupuesto = oCliente.IdGrupoPresupuesto;
            nCliente.MiPyme = oCliente.MiPyme;
            nCliente.Activo = oCliente.Activo;
            nCliente.IdUsuario = oCliente.IdUsuario;
            nCliente.UltimaModificacion = oCliente.UltimaModificacion;           
            context.SaveChanges();

            return nCliente;
        }

       

        /// <summary>
        ///  Ocultar un Cliente
        /// </summary>
        public Cliente OcultarCliente(Cliente oCliente)
        {
            Cliente nCliente = GetClientePorId(oCliente.Id);

            nCliente.Visible =true;

            nCliente.IdUsuario = oCliente.IdUsuario;
            nCliente.UltimaModificacion = oCliente.UltimaModificacion;
            context.SaveChanges();
            return nCliente;
        }




        public Cliente HabilitarCliente(int IdCliente)
        {
            Cliente OCliente = GetClientePorId(IdCliente);
            OCliente.Visible = true;
            OCliente.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return OCliente;
        }

        public Cliente BloquearCliente(int IdCliente)
        {
            Cliente OCliente = GetClientePorId(IdCliente);
            OCliente.Visible = false;
            OCliente.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return OCliente;

        }

        public int DeleteCliente(int IdCliente)
        {
            Cliente OCliente = GetClientePorId(IdCliente);
            OCliente.Activo = false;
            OCliente.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }

        #endregion


        #region "Metodos de Lectura"

        /// <summary>
        ///  Listado completo de todos lo Clientes
        /// </summary>

        public List<Cliente> GetAllCliente()
        {
            context.Configuration.LazyLoadingEnabled = false;

            List<Cliente> listaCliente = context.Cliente
                .Include("TipoCliente")
                .Where(p => p.Activo == true).ToList();
       
            return listaCliente;
        }


       


        /// <summary>
        ///  Obtener un Cliente por el Id
        /// </summary>
        public Cliente GetClientePorId(int IdCliente)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Cliente
                .Include("TipoCliente")
                .Where(p => p.Id == IdCliente).First();
        }


        /// <summary>
        ///  Obtener Listado de Cliente Por Nombre y tipo de cliente
        /// </summary>

     


        /// <summary>
        ///  Obtener Listado de Cliente Por idTipoCliente
        /// </summary>

        public List<Cliente> GetClientePorTipoCliente(int idTipoCliente)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Cliente> listaCliente = context.Cliente
               .Include("TipoCliente")
               .Where(p => p.Activo == true && p.IdTipoCliente == idTipoCliente).ToList();

            return listaCliente;


            //List<Cliente> p = (from c in context.Cliente
            //                   where c.Activo == true &&  c.IdTipoCliente == idTipoCliente
            //                   select c).ToList();
            //return p;

        }

      


        /// <summary>
        ///  obtener el Cliente por el Nombre o Codigo
        /// </summary>
        public List<Cliente> GetClientePorNombre(string strCliente)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Cliente> listaCliente = context.Cliente
              .Include("TipoCliente")
              .Where(p => p.Activo == true && p.Nombre.Contains(strCliente)).ToList();

            return listaCliente;
           
        }

        public List<Cliente> GetClientePorCodigo(string strCodigo)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Cliente> listaCliente = context.Cliente
              .Include("TipoCliente")
              .Where(p => p.Activo == true && p.Codigo.Contains(strCodigo)).ToList();

            return listaCliente;
        }

        public void ActualizarPresupuesto(Cliente model)
        {
            Cliente cliente = GetClientePorId(model.Id);
            cliente.IdGrupoPresupuesto = model.IdGrupoPresupuesto;
            cliente.Activo = true;
            cliente.IdUsuario = model.IdUsuario;
            cliente.UltimaModificacion = model.UltimaModificacion;
            context.SaveChanges();
        }

        public List<Cliente> GetClientePorIdNombre(int idTipoCliente, string strCliente)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Cliente> p = (from c in context.Cliente
                               where c.Activo == true && c.Nombre == strCliente && c.IdTipoCliente == idTipoCliente
                               select c).ToList();
            return p;

        }

        public List<FactVenta> GetFacturasImpagasClientePorId(int idCliente, DateTime fecha)
        {
            return context.FactVenta.Where(f => f.IdCliente == idCliente
                                                && f.Fecha.Day >= fecha.Day
                                                && f.Fecha.Month >= fecha.Month
                                                && f.Fecha.Year >= fecha.Year
                                                && f.Activo == true
                                                && f.TipoComprobanteVenta.Denominacion.Contains("FACTURA")
                                                && f.Saldo > 0).ToList();

        }


        public List<FactVenta> GetFacturasCobradasClientePorId(int idCliente, DateTime fecha)
        {
            List<FactVenta> respuesta = new List<FactVenta>();
            
            return context.FactVenta.Where(f => f.IdCliente == idCliente
                                                && f.Activo == true
                                                && ((f.Fecha.Day >= fecha.Day
                                                && f.Fecha.Month >= fecha.Month
                                                && f.Fecha.Year >= fecha.Year) || f.Saldo != 0)
                                                && f.NumeroCobro > 0).ToList();
        }

        //alldev
        //public List<FactVenta> GetCbtImpagosClientePorId(int idCliente, DateTime fecha)
        public List<FactVenta> GetCbtImpagosClientePorId(int idCliente)
        {
            List<FactVenta> respuesta = new List<FactVenta>();

            /*
            respuesta = context.FactVenta.Where(f => f.IdCliente == idCliente
                                               && f.Activo == true
                                               && (f.Fecha.Day >= fecha.Day
                                               && f.Fecha.Month >= fecha.Month
                                               && f.Fecha.Year >= fecha.Year
                                               || f.Saldo != 0)).ToList();

            */

            //add Edgardo

            respuesta = (List<FactVenta>)context.FactVenta.Where(f => f.IdCliente == idCliente
                                               && f.Activo == true
                                               && f.Saldo != 0)
                                               .OrderBy(f => f.Fecha)
                                                .ToList();
           return (List<FactVenta>)respuesta;
        }
        #endregion



        #region "Reportes"

        // 1 Cuenta Corriente Detalle


        public List<FactVenta> GetCtaCteDetalle(int IdCliente,DateTime fecha)
        {

            context.Configuration.LazyLoadingEnabled = false;
            List<FactVenta> listaCteCteDetalle = context.FactVenta
                                               .Include("TipoComprobanteVenta")
                                               .Include("FacturaElectronica")
                                               .Where(f => f.IdCliente == IdCliente
                                                       && f.Activo == true
                                                       && (f.Fecha >= fecha 
                                                       || f.Saldo != 0))
                                                       .OrderBy(f => f.Fecha)
                                                       .ToList();

            return listaCteCteDetalle;
  
            }


        // 2 Cuenta Corriente Resumen

        public List<CteCteClienteResumen> GetCtaCteResumen(DateTime fechaHasta)
        {
            
// add Edgardo
            var r = context.FactVenta
               .Include("Cliente")
               .Where(p => p.Activo == true &&
               p.Fecha <= fechaHasta &&
               p.Saldo != 0)
               .GroupBy(c => new { c.Cliente.Codigo, c.Cliente.Nombre })
               .Select(c => new CteCteClienteResumen()
               {
                   Codigo = c.Key.Codigo,
                   Nombre = c.Key.Nombre,
                   TotalPesos = c.Sum(x => x.IdMoneda == 1 ? x.Saldo : 0),
                   TotalDolares = c.Sum(x => x.IdMoneda == 2 ? x.Saldo : 0),
                   TotalPesosLocal = c.Sum(x => x.IdMoneda == 1 && x.TipoFac == "L" ? x.Saldo : 0),
                   TotalDolaresLocal = c.Sum(x => x.IdMoneda == 2 && x.TipoFac == "L" ? x.Saldo : 0),
                   FechaUltimoMov = c.Max(x => x.Fecha),
               })
               .OrderBy(resumen => resumen.Codigo) // Ordenar después de la selección  
               .ToList();
            return r;
            
        }


        //3  Registro de Ventas Mensuales

        public List<ConsultaIvaVenta> GetIvaVentas(string Periodo)
        {

                context.Configuration.LazyLoadingEnabled = false;


            int periodo = Convert.ToInt32(Periodo);

            /*
                        var query = from a in context.FactVenta
                                    join s in context.IvaVenta on a.NumeroFactura equals s.NumeroFactura
                                    where a.Activo == true && a.Periodo == periodo
            */

            var query = from a in context.FactVenta
                        join s in context.IvaVenta on new { NumeroFactura = a.NumeroFactura, IdTipoComprobante = a.IdTipoComprobante }
                                                  equals new { NumeroFactura = s.NumeroFactura, IdTipoComprobante = s.IdTipoComprobantes }
                        where a.Activo == true && a.Periodo == periodo


                        select new ConsultaIvaVenta
                        {

                            Abreviatura = a.TipoComprobanteVenta.Abreviatura,
                            CodigoAfip = a.TipoComprobanteVenta.CodigoAfip,
                            PuntoVenta = a.TipoComprobanteVenta.PuntoVenta,
                            NumeroFactura = a.NumeroFactura,
                            Fecha = a.Fecha,
                            Nombre = a.Cliente.Nombre,
                            Cuit = s.Cuit.ToString(),
                            Neto = s.Neto,
                            Gasto = s.Gasto,
                            Iva = s.Iva,
                            Isib = s.Isib,
                            Total = s.Total

                        };


            return query.ToList();


            //var query = from f in context.FactVenta
            //                           join i in context.IvaVenta on f.NumeroFactura equals i.NumeroFactura
            //                           where f.Activo == true
            //                           select new
            //                           {
            //                               f.TipoComprobanteVenta.Abreviatura,
            //                               f.TipoComprobanteVenta.CodigoAfip,
            //                               f.TipoComprobanteVenta.PuntoVenta,
            //                               f.NumeroFactura,
            //                               f.Fecha,
            //                               f.Cliente.Nombre,
            //                               i.Neto,
            //                               i.Gasto,
            //                               i.Iva,
            //                               i.Isib,
            //                               i.Total,
            //                           }).ToList();





        }


        //4  Registro de Ventas Totales

        public ConsultaIvaTotales GetIvaTotales(string Periodo)
        {

            ConsultaIvaTotales lista = new ConsultaIvaTotales();

            int periodo = Convert.ToInt32(Periodo);

            try
            {
                SqlParameter param1 = new SqlParameter("@Periodo", periodo);
                lista = context.Database.SqlQuery<ConsultaIvaTotales>("GetIvaVentasTotalPesos @Periodo", param1).First();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                lista = null;
            }

            return lista;



        }


        //4  Impresion de Iva de Ventas Totales

     
        public List<ConsultaFacturaVentaIva> GetIvaImpresion(string Periodo)
        {


            List <ConsultaFacturaVentaIva> lista = new List<ConsultaFacturaVentaIva>();

            int periodo = Convert.ToInt32(Periodo);

            try
            {
                SqlParameter param1 = new SqlParameter("@Periodo", periodo);
                lista = context.Database.SqlQuery<ConsultaFacturaVentaIva>("GetIvaImpresion @Periodo", param1).ToList();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                lista = null;
            }

            return lista;



        }




        #endregion


    }
}
