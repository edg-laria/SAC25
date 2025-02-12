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
    public class ServicioRetencion : ServicioBase
    {
        private RetencionRepositorio oRetencionRepositorio;

        public ServicioRetencion()
        {
            oRetencionRepositorio = kernel.Get<RetencionRepositorio>();
        }


        public List<RetencionModel> GetAllRetencion()
        {
            return Mapper.Map<List<Retencion>, List<RetencionModel>>(oRetencionRepositorio.GetAllRetencion());
        }


        public RetencionModel GetRetencionOu(int idRetencion)
        {
            return Mapper.Map<Retencion, RetencionModel>(oRetencionRepositorio.GetRetencionOu(idRetencion));
        }

        public List<RetencionModel> GetAllRetencion(int idFactura)
        {
             var a = Mapper.Map<List<Retencion>, List<RetencionModel>>(oRetencionRepositorio.GetAllRetencion(idFactura));
            return a;
        }
        public List<RetencionModel> GetAllRetencionVenta(int idFactura)
        {
            var a = Mapper.Map<List<Retencion>, List<RetencionModel>>(oRetencionRepositorio.GetAllRetencionVenta(idFactura));
            return a;
        }
        public RetencionModel Agregar(RetencionModel oChequeModel)
        {
            try
            {
                var oModel = Mapper.Map<RetencionModel, Retencion>(oChequeModel);
                return Mapper.Map<Retencion, RetencionModel>(oRetencionRepositorio.Agregar(oModel));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

        public RetencionModel Eliminar(RetencionModel oRetencion)
        {
            try
            {
                var oModel = Mapper.Map<RetencionModel, Retencion>(oRetencion);
                return Mapper.Map<Retencion, RetencionModel>(oRetencionRepositorio.Eliminar(oModel));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
          
        }


    }
}
