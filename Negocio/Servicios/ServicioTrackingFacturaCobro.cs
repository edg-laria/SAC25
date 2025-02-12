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
   public class ServicioTrackingFacturaCobro : ServicioBase
    {
        private TrackingFacturaCobroRepositorio TrackingFacturaCobroRepositorio;
      
        public ServicioTrackingFacturaCobro()
        {
            TrackingFacturaCobroRepositorio = kernel.Get<TrackingFacturaCobroRepositorio>();            
        }

        

        #region "Metodos de Actualizacion de Datos"


     

        public TrackingFacturaCobroModel Insert(TrackingFacturaCobroModel model)
        {
            try
            {                
                model.Activo = true;               
                model.UltimaModificacion = DateTime.Now;
                var newModel = TrackingFacturaCobroRepositorio.Insertar(Mapper.Map<TrackingFacturaCobroModel, TrackingFacturaCobroVenta>(model));

                _mensaje?.Invoke("Se registro correctamente", "ok");

                return Mapper.Map<TrackingFacturaCobroVenta, TrackingFacturaCobroModel> (newModel);               
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


