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
   public class ServicioPieNota : ServicioBase
    {
        private PieNotaRepositorio oPieNotaRepositorio;

        public ServicioPieNota()
        {
            oPieNotaRepositorio = kernel.Get<PieNotaRepositorio>();
        }


        #region "Metodos de Actualizacion de Datos"

        public PieNotaModel GuardarPieNota(PieNotaModel model)
        {

            try
            {

                model.Activo = true;
                model.UltimaModificacion = DateTime.Now;
                var newModel = oPieNotaRepositorio.Agregar(Mapper.Map<PieNotaModel, PieNota>(model));
                _mensaje?.Invoke("Se registro correctamente", "ok");
                return Mapper.Map<PieNota, PieNotaModel>(newModel);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "error");
                throw new Exception();

            }


        }


        public PieNotaModel ActualizarPienota(PieNotaModel model)
        {

            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = oPieNotaRepositorio.ActualizarPieNota(Mapper.Map<PieNotaModel, PieNota>(model));
                _mensaje?.Invoke("Se actualizo correctamente", "ok");

                return Mapper.Map<PieNota, PieNotaModel>(newModel);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "error");
                throw new Exception();

            }

        }

        public void Eliminar(int IdPieNota)
        {
            try
            {


                var retorno = oPieNotaRepositorio.DeletePieNota(IdPieNota);
                _mensaje?.Invoke("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }




        public void BloquearPieNota(int IdPieNota)


        {
            try
            {


                var retorno = oPieNotaRepositorio.BloquearPieNota(IdPieNota);
                _mensaje?.Invoke("Se Ocultó Correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }
        }

        public void HabilitarPieNota(int IdPieNota)
        {
            try
            {


                var retorno = oPieNotaRepositorio.HabilitarPieNota(IdPieNota);
                _mensaje?.Invoke("Se Habilitó Correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }
        }




        #endregion


        #region "Metodos de lectura de Datos"

        /// <summary>
        ///  Listado completo de todos los Pie de Nota
        /// </summary>

        public List<PieNotaModel> GetAllPieNota()
        {
            return Mapper.Map<List<PieNota>, List<PieNotaModel>>(oPieNotaRepositorio.GetAllPieNota());
        }



        /// <summary>
        ///  Obtener un PieNota por el Id
        /// </summary>


        public PieNotaModel GetPieNotaPorId(int id)
        {
            try
            {
                return Mapper.Map<PieNota, PieNotaModel>(oPieNotaRepositorio.GetPieNotaPorId(id));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");

                return null;
            }
        }

        public PieNotaModel GetPieNotaPorCodigo(string codigo)
        {
            try
            {
                return Mapper.Map<PieNota, PieNotaModel>(oPieNotaRepositorio.GetPieNotaPorCodigo(codigo));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");

                return null;
            }
        }
        #endregion
    }
}

