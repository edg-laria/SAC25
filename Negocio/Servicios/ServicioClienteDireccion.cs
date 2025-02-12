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
using System.Data.Entity.Validation;

namespace Negocio.Servicios
{
   public class ServicioClienteDireccion : ServicioBase
    {
        private ClienteDireccionRepositorio oClienteDireccionRepositorio;

      
     

        public ServicioClienteDireccion()
        {
            oClienteDireccionRepositorio = kernel.Get<ClienteDireccionRepositorio>();
        }




        #region "Metodos de Actualizacion de Datos"

        public ClienteDireccionModel ActualizarDireccion(ClienteDireccionModel model)
        {

            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = oClienteDireccionRepositorio.ActualizarDireccion(Mapper.Map<ClienteDireccionModel, ClienteDireccion>(model));
                _mensaje?.Invoke("Se actualizo correctamente", "ok");

                return Mapper.Map<ClienteDireccion, ClienteDireccionModel>(newModel);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "error");
                throw new Exception();

            }

        }

        public void Eliminar(int IdDireccion)
        {
            try
            {


                var retorno = oClienteDireccionRepositorio.DeleteDireccion(IdDireccion);
                _mensaje?.Invoke("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }

        public ClienteDireccionModel GuardarDireccion(ClienteDireccionModel model)
        {

            try
            {
                model.Activo = true;
                model.UltimaModificacion = DateTime.Now;
                var newModel = oClienteDireccionRepositorio.Agregar(Mapper.Map<ClienteDireccionModel, ClienteDireccion>(model));
                _mensaje?.Invoke("Se registro correctamente", "ok");
                return Mapper.Map<ClienteDireccion, ClienteDireccionModel>(newModel);
            }
            /*
                      catch (Exception ex)
                      {
                          _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "error");
                          throw new Exception();

                      }
            */
            //add Edgardo
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                ServicioElog.Log(this, e);
                return null;
            }


        }

        #endregion

        public List<ClienteDireccionModel> GetDireccionPorcliente(int Idcliente)
        {

            try
            {
                return Mapper.Map<List<ClienteDireccion>, List<ClienteDireccionModel>>(oClienteDireccionRepositorio.GetDireccionPorcliente(Idcliente));
            }
            catch (Exception e)
            {                
               ServicioElog.Log(this, e);
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }


        }


        public ClienteDireccionModel ObtenerPorID(int id)
        {
            try
            {
                return Mapper.Map<ClienteDireccion, ClienteDireccionModel>(oClienteDireccionRepositorio.GetObtenerDireccion(id));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");                               
                return null;
            }

        }

      
    }
}
