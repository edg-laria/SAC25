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
   public class ServicioContable : ServicioBase
    {
        private DiarioRepositorio diarioRepositorio;      
        private ServicioImputacion servicioImputacion = new ServicioImputacion();
        public ServicioContable()
        {
            diarioRepositorio = kernel.Get<DiarioRepositorio>();
        }

        public List<DiarioModel> GetAllDiario()
        {
            try
            {
                return Mapper.Map<List<Diario>, List<DiarioModel>>(diarioRepositorio.GetAllDiario());
                 
            }
            catch (Exception)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                throw new Exception();
            }
        }
      
        public Diario InsertAsientoContable(string alias, decimal total, DiarioModel asiento, CompraFacturaModel facturaRegistrada, int idImputacion = 0)
        {
            try
            {
                ImputacionModel imputacionModel;
                if (idImputacion > 0)
                {
                    imputacionModel = servicioImputacion.GetImputacion(idImputacion);
                }
                else
                {
                    imputacionModel = servicioImputacion.GetImputacionPorAlias(alias);
                }
                if (imputacionModel !=null) { 
                asiento.IdImputacion = imputacionModel.Id;

                    //asiento.Importe = (facturaRegistrada.IdMoneda == 1) ? ( total) : (total * facturaRegistrada.Cotizacion);
                    asiento.Importe =  total;
                    asiento.Descripcion = imputacionModel.Descripcion;
                asiento.Titulo = asiento.Titulo ??  imputacionModel.Descripcion;
                Diario asientoContable = diarioRepositorio.InsertarDiario(Mapper.Map<DiarioModel, Diario>(asiento));

                return asientoContable;
                }
                return null;
            }
            catch (Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioContable >> InsertAsientoContable");
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                throw new Exception("No pudo registar el Asiento Contable");                            
            }
        }


        public Diario InsertAsientoContable(string alias, DiarioModel asiento, int idImputacion = 0)
        {
            try
            {
                ImputacionModel imputacionModel;
                if (idImputacion > 0)
                {
                    imputacionModel = servicioImputacion.GetImputacion(idImputacion);
                }
                else
                {
                    imputacionModel = servicioImputacion.GetImputacionPorAlias(alias);
                }
                if (imputacionModel != null)
                {
                    asiento.IdImputacion = imputacionModel.Id;
                   
                    Diario asientoContable = diarioRepositorio.InsertarDiario(Mapper.Map<DiarioModel, Diario>(asiento));

                    return asientoContable;
                }
                return null;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                throw new Exception();
            }
        }



        public int GetNuevoCodigoAsiento()
        {
            try
            {
                return diarioRepositorio.GetNuevoCodigoAsiento();

            }
            catch (Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioContable >> GetNuevoCodigoAsiento");               
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                 throw new Exception("Error en obtener el codigo de asiento");
            }
        }
       
    }
}
