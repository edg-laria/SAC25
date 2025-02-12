using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using Negocio.Modelos;
using AutoMapper;
using System.Net.Mail;
using System.IO;
using System.Net;
using Negocio.Servicios;
using System.Net.Mime;
using System.Text;
using Negocio.Helpers;

namespace Negocio.Servicios
{
   public class ServicioCaja : ServicioBase
    {
        private CajaRepositorio CajaRepositorio ;
        private TarjetaOperacionRepositorio otarjetaoperacionrepositorio ;


        public ServicioCaja()
        {
            CajaRepositorio = kernel.Get<CajaRepositorio>();
            otarjetaoperacionrepositorio = kernel.Get<TarjetaOperacionRepositorio>();
        }

        #region "Metodos de Lectura de Datos"


        public List<CajaModel> GetAllCaja()
        {
            try
            {
                var Caja = Mapper.Map<List<Caja>, List<CajaModel>>(CajaRepositorio.GetAllCaja());
                return Caja;

            }
            catch (Exception e)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }

      
        public List<CajaModel> GetAllCajaPorIdCierre(int v)
        {
            try
            {
                return Mapper.Map<List<Caja>, List<CajaModel>>(CajaRepositorio.GetAllCajaPorIdCierre(v));               
            }
            catch (Exception e)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }

        public CajaModel GetCajaPorId(int id)
        {
            try
            {
                return Mapper.Map<Caja, CajaModel>(CajaRepositorio.GetCajaPorId(id));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }


        public CajaModel GetCajaPorCodigo(int idcodigocaja )
        {
            try
            {
                return Mapper.Map<Caja, CajaModel>(CajaRepositorio.GetCajaPorGrupo(idcodigocaja));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }

        #endregion

        #region "Metodos de Actualizacion de Datos"

        public void Eliminar(int IdCaja)
        {
            try
            {
                var retorno = CajaRepositorio.DeleteCaja(IdCaja);
                _mensaje?.Invoke("Se eliminó correctamente", "ok");
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();
            }
        }



     

        public CajaModel GuardarCaja(CajaModel model)
        {
            try
            {                
                model.Activo = true;               
                model.UltimaModificacion = DateTime.Now;



                var newModel = CajaRepositorio.Insertar(Mapper.Map< CajaModel,Caja>(model));

                _mensaje?.Invoke("Se registro correctamente", "ok");

                // Si viene con valor la tarjeta
                //if (model.IdTarjeta > 0 && model.ImporteTarjeta > 0)
                //{
                //    InsertarOperacionTarjeta(model);
                //}
                // Si viene con valor el cheque
                //if (model.IdCheque > 0 && model.ImporteCheque > 0)
                //{
                //    //InsertarOperacionTarjeta(model);
                //}





                return Mapper.Map<Caja,CajaModel> (newModel);               
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }
        }

        /// <summary>
        /// Resgitrando Pago de Factura Compras
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CajaModel IngresoPagoCaja(CajaModel model)
        {
            try
            {
                model.Activo = true;
                model.UltimaModificacion = DateTime.Now;
                var newModel = CajaRepositorio.Insertar(Mapper.Map<CajaModel, Caja>(model));
                _mensaje?.Invoke("Se registro correctamente", "ok");
                return Mapper.Map<Caja, CajaModel>(newModel);
            }
            catch (Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioCaja >> IngresoPagoCaja");
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();
            }
        }

        public CajaModel ActualizarCaja(CajaModel model)
        {

            try
            {

                //if (model.IdTarjeta > 0)
                //{

                //    InsertarOperacionTarjeta(model);
                //}

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = CajaRepositorio.ActualizarCaja(Mapper.Map<CajaModel, Caja>(model));              
                _mensaje?.Invoke("Se actualizo correctamente", "ok");
                
                return Mapper.Map<Caja, CajaModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }

        public CajaModel RegistroManualDeCaja(CajaModel model)
        {
            try
            {
                model.UltimaModificacion = DateTime.Now;
                var newModel = CajaRepositorio.Insertar(Mapper.Map<CajaModel, Caja>(model));
                _mensaje?.Invoke("Se registro correctamente", "ok");             
                return Mapper.Map<Caja, CajaModel>(newModel);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();
            }
        }

        public TarjetaOperacionModel InsertarOperacionTarjeta(TarjetaOperacionModel model)
        {                          
            var tarjetaOperacion = Mapper.Map<TarjetaOperacionModel, TarjetaOperacion>(model);
            //_mensaje?.Invoke("El cheque se ingresó correctamente", "ok");
            return Mapper.Map<TarjetaOperacion, TarjetaOperacionModel>(otarjetaoperacionrepositorio.Agregar(tarjetaOperacion));

        }




        public List<CajaModel> getGrupoCajaFecha(int idgrupocaja, DateTime fechadesde, DateTime fechahasta)
        {

            var Caja = Mapper.Map<List<Caja>, List<CajaModel>>(CajaRepositorio.getGrupoCajaFecha(idgrupocaja,fechadesde,fechahasta));
            return Caja;

        }

        public List<CajaModel> GetSaldoInicialCaja(int cIdGrupoCaja, DateTime cfechadesde)

        {

            var Caja = Mapper.Map<List<Caja>, List<CajaModel>>(CajaRepositorio.GetSaldoInicialCaja(cIdGrupoCaja, cfechadesde));
            return Caja;
        }

        public CajaModel GetCajaPorFecha(int idgrupocaja ,DateTime fecha)
        {
            try
            {
                return Mapper.Map<Caja, CajaModel>(CajaRepositorio.GetCajaPorFecha(idgrupocaja, fecha));
            }
            catch (Exception e)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }

        
  public CajaModel GetCajaMenorIgualAFecha(int idgrupocaja, DateTime fecha)
        {
            try
            {
                return Mapper.Map<Caja, CajaModel>(CajaRepositorio.GetCajaPorFecha(idgrupocaja, fecha));
            }
            catch (Exception e)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }

        public void ActualizarCierreCaja(CajaModel model)
        {
            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                 CajaRepositorio.ActualizarCierreCaja(Mapper.Map<CajaModel, Caja>(model));
                _mensaje?.Invoke("Se actualizo correctamente", "ok");
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error - ActualizarCierreCaja. contacte al administrador", "error");
                throw new Exception();

            }
        }

     

        public CajaModel IngresoCuentaBancaria(BancoCuentaBancariaModel model ,int IdGrupoCaja)
        {

            try
            {
                CajaModel caja = new CajaModel();
                caja.IdTipoMovimiento = 1;
                caja.IdCuentaBanco = model.IdBancoCuenta;
                caja.Concepto = model.CuentaDescripcion;
                caja.ImporteCheque = model.Importe;
                caja.Fecha = model.Fecha;
                caja.IdGrupoCaja = IdGrupoCaja; // 'BANCH'


                return GuardarCaja(caja);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error - ActualizarCierreCaja. contacte al administrador", "error");
                throw new Exception();

            }
        }



        #endregion
    }

}


