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

namespace Negocio.Servicios
{
   public class ServicioCajaSaldo : ServicioBase
    {
        private CajaSaldoRepositorio CajaSaldoRepositorio ;
      
        public ServicioCajaSaldo()
        {
            CajaSaldoRepositorio = kernel.Get<CajaSaldoRepositorio>();
            
        }


        #region "Metodos de Lectura de Datos"


        public List<CajaSaldoModel> GetAllCajaSaldo()
        {
            try
            {
                var Caja = Mapper.Map<List<CajaSaldo>, List<CajaSaldoModel>>(CajaSaldoRepositorio.GetAllCajaSaldo());
                return Caja;

            }
            catch (Exception e)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }

        public CajaSaldoModel GetUltimoCierre()
        {

            try
            {
                CajaSaldoModel cajaSaldoModel = new CajaSaldoModel();
                 var saldoCaja= Mapper.Map<CajaSaldo, CajaSaldoModel>(CajaSaldoRepositorio.GetUltimoCierre());
                if (saldoCaja != null)
                {
                    cajaSaldoModel = saldoCaja;
                }

                return cajaSaldoModel;
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }


        }

        public CajaSaldoModel GetCajaSaldoPorId(int id)
        {
            try
            {
                return Mapper.Map<CajaSaldo, CajaSaldoModel>(CajaSaldoRepositorio.GetCajaSaldoPorId(id));
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


                var retorno = CajaSaldoRepositorio.DeleteCaja(IdCaja);
                _mensaje?.Invoke("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }

        public int GetNuevoNumeroCierre()
        {
            try
            {
                return CajaSaldoRepositorio.GetNuevoNumeroCierre();
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return 0;
            }
            //throw new NotImplementedException();
        }

        public CajaSaldoModel GuardarCajaSaldo(CajaSaldoModel model)
        {

            try
            {
                
                model.Activo = true;               
                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now);
                var newModel = CajaSaldoRepositorio.Insertar(Mapper.Map< CajaSaldoModel,CajaSaldo>(model));
                _mensaje?.Invoke("Se registro correctamente", "ok");
                return Mapper.Map<CajaSaldo,CajaSaldoModel> (newModel);               
            }
            catch (Exception  ex)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "error");
                throw new Exception();

            }


        }



        public CajaSaldoModel ActualizarCajaSaldo(CajaSaldoModel model)
        {
            try
            {
                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = CajaSaldoRepositorio.ActualizarCajaSaldo(Mapper.Map<CajaSaldoModel, CajaSaldo>(model));              
                _mensaje?.Invoke("Se actualizo correctamente", "ok");
                
                return Mapper.Map<CajaSaldo, CajaSaldoModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }

        public CajaSaldoModel ActualizarImporteCierreCajaSaldo(CajaSaldoModel model)
        {
            try
            {
                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                CajaSaldo newModel = CajaSaldoRepositorio.ActualizarImporteCierreCajaSaldo(Mapper.Map<CajaSaldoModel, CajaSaldo>(model));
                _mensaje?.Invoke("Se actualizo correctamente", "ok");
                return Mapper.Map<CajaSaldo, CajaSaldoModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }
        }


        #endregion
    }

}


