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
   public class ServicioGrupoPresupuesto : ServicioBase
    {
        private GrupoPresupuestoRepositorio oGrupoPresupuestoRepositorio;
      
        public ServicioGrupoPresupuesto()
        {
            oGrupoPresupuestoRepositorio = kernel.Get<GrupoPresupuestoRepositorio>();
            
        }


        #region "Metodos de Actualizacion de Datos"

        public void Eliminar(int IdGrupoPresupuesto)
        {
            try
            {


                var retorno = oGrupoPresupuestoRepositorio.DeleteGrupoPresupuesto(IdGrupoPresupuesto);
                _mensaje?.Invoke("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }





        public GrupoPresupuestoModel GuardarGrupoPresupuesto(GrupoPresupuestoModel model)
        {

            try
            {

                model.Activo = true;
                model.UltimaModificacion = DateTime.Now;
                var newModel = oGrupoPresupuestoRepositorio.Insertar(Mapper.Map<GrupoPresupuestoModel, GrupoPresupuesto>(model));
                _mensaje?.Invoke("Se registro correctamente", "ok");
                return Mapper.Map<GrupoPresupuesto, GrupoPresupuestoModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }


        }


        public GrupoPresupuestoModel ActualizarGrupoPresupuesto(GrupoPresupuestoModel model)
        {

            try
            {
                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = oGrupoPresupuestoRepositorio.ActualizarGrupoPresupuesto(Mapper.Map<GrupoPresupuestoModel, GrupoPresupuesto>(model));
                _mensaje?.Invoke("Se actualizo correctamente", "ok");

                return Mapper.Map<GrupoPresupuesto, GrupoPresupuestoModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }

      


        #endregion


        #region "Metodos de Lectura de Datos"


        public List<GrupoPresupuestoModel> GetAllGrupoPresupuesto()
        {
            try
            {
                var Grupo = Mapper.Map<List<GrupoPresupuesto>, List<GrupoPresupuestoModel>>(oGrupoPresupuestoRepositorio.GetAllGrupoPresupuesto());
                return Grupo;

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }
        //public List<PresupuestoActualModel> GetAllGrupoPresupuesto()
        //{
        //    try
        //    {
        //        return Mapper.Map<List<PrespuestoActual>, List<PresupuestoActualModel>>(oGrupoPresupuestoRepositorio.GetAllGrupoPresupuesto());

        //    }
        //    catch (Exception)
        //    {
        //        _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
        //        return null;
        //    }
        //}


        public GrupoPresupuestoModel GetGrupoGrupoPresupuestoPorId(int id)
        {
            try
            {
                return Mapper.Map<GrupoPresupuesto, GrupoPresupuestoModel>(oGrupoPresupuestoRepositorio.GetGrupoGrupoPresupuestoPorId(id));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }


      

        #endregion

       
    }

}


