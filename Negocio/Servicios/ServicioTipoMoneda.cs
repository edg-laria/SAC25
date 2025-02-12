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
   public class ServicioTipoMoneda : ServicioBase
    {
        private TipoMonedaRepositorio oTipoMonedaRepositorio;
        private ValorCotizacionRepositorio valorCotizacionRepositorio;
        public ServicioTipoMoneda()
        {
            oTipoMonedaRepositorio = kernel.Get<TipoMonedaRepositorio>();
            valorCotizacionRepositorio = kernel.Get<ValorCotizacionRepositorio>();
        }

        public List<TipoMonedaModel> GetAllTipoMonedas(int id)
        {
            try
            {
                var TipoMonedas = Mapper.Map<List<TipoMoneda>, List<TipoMonedaModel>>(oTipoMonedaRepositorio.GetAllTipoMoneda());
                return TipoMonedas;
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public TipoMonedaModel ObtenerTipoMonedaPorNombre(string moneda)
        {
            try
            {
                return Mapper.Map<TipoMoneda, TipoMonedaModel>(oTipoMonedaRepositorio.ObtenerTipoMonedaPorNombre(moneda));
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public void GuardarCotizacionMoneda(ValorCotizacionModel valorCotizacionModel)
        {
            try
            {
                valorCotizacionRepositorio.Insertar(Mapper.Map<ValorCotizacionModel, ValorCotizacion>(valorCotizacionModel));
                _mensaje?.Invoke("Se Actualizo la Cotizacion de Monedas segun BNA", "ok");

            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
               
            }
        }

        public List<ValorCotizacionModel> ObtenerCotizacion(DateTime fecha)
        {
            try
            {
                var Cotiza = Mapper.Map<List<ValorCotizacion>, List<ValorCotizacionModel>>(valorCotizacionRepositorio.GetCotizacionMoneda(fecha));
                return Cotiza;
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public void updateCotizacionPorIdMoneda(ValorCotizacionModel valorCotizacionModel)
        {
            try
            {
                valorCotizacionRepositorio.updateCotizacionPorIdMoneda(Mapper.Map<ValorCotizacionModel, ValorCotizacion>(valorCotizacionModel));
                _mensaje?.Invoke("Se actualizo la cotización de Moneda", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");

            }
        }

        public List<TipoMonedaModel> GetAllTipoMonedas()
        {
            try
            {
                var TipoMonedas = Mapper.Map<List<TipoMoneda>, List<TipoMonedaModel>>(oTipoMonedaRepositorio.GetAllTipoMoneda());
                return TipoMonedas;
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }
       
        public TipoMonedaModel GetTipoMoneda(int _id)
        {
            TipoMoneda oTipoMoneda = oTipoMonedaRepositorio.ObtenerTipoMonedaPorId(_id);
            TipoMonedaModel oTipoMonedaModel = new TipoMonedaModel();

            oTipoMonedaModel.Id = oTipoMoneda.Id;
            oTipoMonedaModel.Descripcion = oTipoMoneda.Descripcion;
            oTipoMonedaModel.Activo = oTipoMoneda.Activo;

            return oTipoMonedaModel;
        }

        public int ActualizarPais(TipoMonedaModel oTipoMonedaModel)
        {
            //controlar que no exista 
            TipoMoneda oTipoMoneda = oTipoMonedaRepositorio.ObtenerTipoMonedaPorNombre(oTipoMonedaModel.Descripcion, oTipoMonedaModel.Id);
            if (oTipoMoneda != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                TipoMoneda oPaisNuevo = new TipoMoneda();
                TipoMoneda oPaisRespuesta = new TipoMoneda();

                oPaisNuevo.Id = oTipoMonedaModel.Id;
                oPaisNuevo.Descripcion = oTipoMonedaModel.Descripcion;
                oPaisNuevo.Activo = true;

                oPaisRespuesta = oTipoMonedaRepositorio.ActualizarTipoMoneda(oPaisNuevo);

                if (oPaisRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }
        public ValorCotizacionModel GetCotizacionPorIdMoneda(int idMoneda)
        {
            try
            {
                return Mapper.Map<ValorCotizacion, ValorCotizacionModel>(valorCotizacionRepositorio.GetCotizacionPorIdMoneda(idMoneda));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }
        public ValorCotizacionModel GetCotizacionPorIdMoneda(DateTime f, int idMoneda)
        {
            try
            {
                return Mapper.Map<ValorCotizacion, ValorCotizacionModel>(valorCotizacionRepositorio.GetCotizacionPorIdMoneda(f, idMoneda));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke(ex.Message, "error");
                return null;
            }           
        }

        public int GuardarTipoMoneda(TipoMonedaModel oTipoMonedaModel)
        {
            //controlar que no exista 
            TipoMoneda oTipoMoneda = oTipoMonedaRepositorio.ObtenerTipoMonedaPorNombre(oTipoMonedaModel.Descripcion);
            if (oTipoMoneda != null)
            {
                return -2;
            }
            else
            {
                TipoMoneda oTipoMonedaNuevo = new TipoMoneda();
                TipoMoneda oTipoMonedaRespuesta = new TipoMoneda();

                oTipoMonedaNuevo.Descripcion = oTipoMonedaModel.Descripcion;
                oTipoMonedaNuevo.Activo = true;
                oTipoMonedaNuevo.IdUsuario = oTipoMonedaModel.IdUsuario;
                oTipoMonedaNuevo.UltimaModificacion = oTipoMonedaModel.UltimaModificacion;

                oTipoMonedaRespuesta = oTipoMonedaRepositorio.InsertarTipoMoneda(oTipoMonedaNuevo);
                if (oTipoMonedaRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int Eliminar(int idTipoMoneda)
        {
            var retorno = oTipoMonedaRepositorio.EliminarTipoMoneda(idTipoMoneda);
            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }

    }
}
