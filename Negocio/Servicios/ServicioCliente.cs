using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;

namespace Negocio.Servicios
{
   public class ServicioCliente : ServicioBase
    {
        private ClienteRepositorio oClienteRepositorio;
       

        public ServicioCliente()
        {
            oClienteRepositorio = kernel.Get<ClienteRepositorio>();
        }


        public List<ClienteModel> GetAllCliente()
        {

            try
            {
                List<ClienteModel> listaCheque = Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetAllCliente());

           

            return listaCheque;
            }
            catch (Exception )
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }


        }



        public List<ClienteModel> GetClientePorNombre(string strCliente)
        {
            try
            {
                return Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetClientePorNombre(strCliente));
            }
            catch (Exception )
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }
       
        public List<ClienteModel> GetClientePorCodigo(string strCodigo)
        {
            try
            {
                return Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetClientePorCodigo(strCodigo));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }


        public ClienteModel GetClientePorId(int id)
        {
            try
            {
                return Mapper.Map<Cliente, ClienteModel>(oClienteRepositorio.GetClientePorId(id));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + ex.Message, "error" );

                return null;
            }
        }


        public List<ClienteModel> GetClientePorTipoCliente(int IdTipoCliente)
        {

            try
            {
                return Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetClientePorTipoCliente(IdTipoCliente));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }


        }

        public List<ClienteModel> GetClientePorIdNombre(int IdTipoCliente, string strCliente)
        {

            try
            {
                return Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetClientePorIdNombre(IdTipoCliente,  strCliente));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }


        }

       
        public ClienteModel ActualizarCliente(ClienteModel model)
        {

            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = oClienteRepositorio.ActualizarCliente(Mapper.Map<ClienteModel, Cliente>(model));
                _mensaje?.Invoke("Se actualizo correctamente", "ok");

                return Mapper.Map<Cliente, ClienteModel>(newModel);
            }



            catch (System.Data.Entity.Validation.DbEntityValidationException ex)

            {
                string mensaje = "";

                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        //System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        mensaje += "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage;

                       
                    }
                }

               // _mensaje?.Invoke(mensaje);
                throw new Exception(mensaje);
            }



            //catch (Exception ex)
            //{
            //    _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "error");
            //    throw new Exception();

            //}

        }

        public void Eliminar(int IdCliente)
        {
            try
            {


                var retorno = oClienteRepositorio.DeleteCliente(IdCliente);
                _mensaje?.Invoke("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }


        public void BloquearCliente(int IdCliente)
        {
            try
            {
                var retorno = oClienteRepositorio.BloquearCliente(IdCliente);
                _mensaje?.Invoke("Se bloqueó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }
        }

        public void HabilitarCliente(int IdCliente)
        {
            try
            {


                var retorno = oClienteRepositorio.HabilitarCliente(IdCliente);
                _mensaje?.Invoke("Se Habilitó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }
        }

        public ClienteModel GuardarCliente(ClienteModel model)
        {

            try
            {

                model.Activo = true;
                model.UltimaModificacion = DateTime.Now;
                var newModel = oClienteRepositorio.Agregar(Mapper.Map<ClienteModel, Cliente>(model));
                _mensaje?.Invoke("Se registro correctamente", "ok");
                return Mapper.Map<Cliente, ClienteModel>(newModel);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "error");
                throw new Exception();

            }


        }

        internal void ActualizarPresupuestoCliente(ClienteModel model)
        {
            try
            {
                //if (_mensaje != null) { 
                //    _mensaje?.Invoke("El proveedor se actualizo correctamente", "ok");
                //}
                oClienteRepositorio.ActualizarPresupuesto(Mapper.Map<ClienteModel, Cliente>(model));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurrido un error. Contactese con el Administrador", "error");
                throw new Exception("No pudo ejecutar ActualizarPresupuestoCliente - " + ex);
            }
        }





        #region "Servicios Reportes"

        // 1 Cuenta Corriente Detalle






        public List<CobroFacturaModel> GetCtaCteDetalle(int IdCliente,DateTime strCodigo)
        {
            try
            {               
                var model = Mapper.Map<List<FactVenta>, List<CobroFacturaModel>>(oClienteRepositorio.GetCtaCteDetalle(IdCliente, strCodigo));
                return model;

            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        
        public List<FactVenta> GetFacturasCobradasClientePorId(int idCliente, DateTime fechaHasta)
        {
            try
            {
                return oClienteRepositorio.GetFacturasCobradasClientePorId(idCliente, fechaHasta);
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

     public List<FactVenta> GetFacturasImpagasClientePorId(int IdCliente, DateTime fecha)
        {
            try
            {
                return oClienteRepositorio.GetFacturasImpagasClientePorId(IdCliente, fecha);               
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }
        //public List<FactVenta> GetCbtImpagosClientePorId(int IdCliente, DateTime fecha)
        public List<FactVenta> GetCbtImpagosClientePorId(int IdCliente)
        {
            try
            {
                // return oClienteRepositorio.GetCbtImpagosClientePorId(IdCliente, fecha);
                return oClienteRepositorio.GetCbtImpagosClientePorId(IdCliente);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex )
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador ", "error");
                return null;
            }
        }

        // 2 Cuenta Corriente Resumen

        public List<CteCteClienteResumenModel> GetCtaCteResumen(DateTime fechaHasta)
        {
            try
            {
                var datos = oClienteRepositorio.GetCtaCteResumen(fechaHasta);

                return datos.ToList().Select(i =>  new  CteCteClienteResumenModel()
                          {
                               Codigo = i.Codigo,
                               Nombre = i.Nombre,
                                TotalPesos = i.TotalPesos- i.TotalPesosLocal,
                                TotalDolares = i.TotalDolares - i.TotalDolaresLocal,
                                TotalDolaresLE = i.TotalDolares,
                                TotalPesosLocal = i.TotalPesosLocal,
                                TotalDolaresLocal = i.TotalDolaresLocal,
                                FechaUltimoMov = i.FechaUltimoMov
                           
                           }).ToList();                              
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                throw new Exception();

            }
        }



        //3  Registro de Ventas Mensuales

    

        public List<ConsultaIvaVentaModel> GetIvaVentas(string Periodo)
        {
            try
            {
                return Mapper.Map<List<ConsultaIvaVenta>, List<ConsultaIvaVentaModel>>(oClienteRepositorio.GetIvaVentas(Periodo));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }


        //4  Registro de Ventas Totales
       
             public ConsultaIvaTotalesModel GetIvaVentasTotales(string Periodo)
        {
            try
            {
                return Mapper.Map<ConsultaIvaTotales, ConsultaIvaTotalesModel>(oClienteRepositorio.GetIvaTotales(Periodo));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + ex.Message, "error");

                return null;
            }
        }




        //5  Impresion de Iva

        public List<FacturaVentaIvaModel> GetIvaImpresion(string Periodo)
        {
            try
            {
                return Mapper.Map<List<ConsultaFacturaVentaIva>, List<FacturaVentaIvaModel>>(oClienteRepositorio.GetIvaImpresion(Periodo));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + ex.Message, "error");

                return null;
            }
        }


      


        #endregion




    }
}
