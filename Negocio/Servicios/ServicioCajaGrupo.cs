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
   public class ServicioCajaGrupo : ServicioBase
    {
        private CajaGrupoRepositorio cajaGrupoRepositorio;
      
        public ServicioCajaGrupo()
        {
            cajaGrupoRepositorio = kernel.Get<CajaGrupoRepositorio>();
            
        }



        #region "Metodos de Lectura de Datos"


        public List<CajaGrupoModel> GetAllCajaGrupo()
        {
            try
            {
                var CajaGrupo = Mapper.Map<List<GrupoCaja>, List<CajaGrupoModel>>(cajaGrupoRepositorio.GetAllGrupoCaja());
                return CajaGrupo;
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }



        public CajaGrupoModel GetGrupoCajaPorId(int id)
        {
            try
            {
                return Mapper.Map<GrupoCaja, CajaGrupoModel>(cajaGrupoRepositorio.GetGrupoCajaPorId(id));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }


        public CajaGrupoModel GetGrupoCajaPorCodigo(string codigo )
        {
            try
            {
                return Mapper.Map<GrupoCaja, CajaGrupoModel>(cajaGrupoRepositorio.GetGrupoCajaPorCodigo(codigo));
            }
            catch (Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioCajaGrupo >> GetGrupoCajaPorCodigo");            
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }

        #endregion

        #region "Metodos de Actualizacion de Datos"

        public void Eliminar(int IdGrupoCaja)
        {
            try
            {


                var retorno = cajaGrupoRepositorio.DeleteGrupoCaja(IdGrupoCaja);
                _mensaje?.Invoke("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }



     

        public CajaGrupoModel GuardarGrupoCaja(CajaGrupoModel model)
        {

            CajaGrupoModel modelo = new CajaGrupoModel();
            try
            {

                modelo = GetGrupoCajaPorCodigo(model.Codigo);


                if (model.IdImputacion==0 )

                {
                    _mensaje?.Invoke("Debe seleccionar un Numero de Imputacion", "error");

                    return Mapper.Map<GrupoCaja, CajaGrupoModel>(cajaGrupoRepositorio.GetGrupoCajaPorCodigo(model.Codigo));

                }




                if (modelo != null)
                {
                   
                

                    _mensaje?.Invoke("El codigo que intenta Ingresar ya Existe", "error");

                    return Mapper.Map<GrupoCaja, CajaGrupoModel>(cajaGrupoRepositorio.GetGrupoCajaPorCodigo(model.Codigo));

                }

                else


                {

                    model.Activo = true;
                    model.UltimaModificacion = DateTime.Now;
                    var newModel = cajaGrupoRepositorio.Insertar(Mapper.Map<CajaGrupoModel, GrupoCaja>(model));
                    _mensaje?.Invoke("Se registro correctamente", "ok");

                    return Mapper.Map<GrupoCaja, CajaGrupoModel>(newModel);


                }



            }
            catch (Exception )
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }


        }



        public CajaGrupoModel ActualizarGrupoCaja(CajaGrupoModel model)
        {

            try
            {

                if (model.IdImputacion == 0)

                {
                    _mensaje?.Invoke("Debe seleccionar un Numero de Imputacion", "error");

                    return Mapper.Map<GrupoCaja, CajaGrupoModel>(cajaGrupoRepositorio.GetGrupoCajaPorCodigo(model.Codigo));

                }



                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = cajaGrupoRepositorio.ActualizarGrupoCaja(Mapper.Map<CajaGrupoModel, GrupoCaja>(model));              
                _mensaje?.Invoke("Se actualizo correctamente", "ok");
                
                return Mapper.Map<GrupoCaja, CajaGrupoModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }

        public void CerrarGrupoCaja()
        {

            try
            {


                var retorno = cajaGrupoRepositorio.CerrarGrupoCaja();
            _mensaje?.Invoke("Se Cerro correctamente el Grupo Caja", "ok");


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


