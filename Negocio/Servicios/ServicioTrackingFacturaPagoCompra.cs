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
   public class ServicioTrackingFacturaPagoCompra : ServicioBase
    {
        private TrackingFacturaPagoCompraRepositorio TrackingFacturaPagoCompraRepositorio;
      
        public ServicioTrackingFacturaPagoCompra()
        {
            TrackingFacturaPagoCompraRepositorio = kernel.Get<TrackingFacturaPagoCompraRepositorio>();            
        }

        

        #region "Metodos de Actualizacion de Datos"


     

        public TrackingFacturaPagoCompraModel Insert(TrackingFacturaPagoCompraModel model)
        {

            try
            {
                
                model.Activo = true;               
                model.UltimaModificacion = DateTime.Now;
                var newModel = TrackingFacturaPagoCompraRepositorio.Insertar(Mapper.Map<TrackingFacturaPagoCompraModel, TrackingFacturaPagoCompra>(model));

                _mensaje?.Invoke("Se registro correctamente", "ok");

                return Mapper.Map<TrackingFacturaPagoCompra, TrackingFacturaPagoCompraModel> (newModel);               
            }
            catch (Exception )
            {
                //_mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception("Ops!, Ha ocurriodo un error. contacte al administrador");

            }


        }





        #endregion
    }

}


