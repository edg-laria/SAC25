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

namespace Negocio.Servicios
{
   public class ServicioBancoCuentaBancaria: ServicioBase
    {
        private BancoCuentaBancariaRepositorio oBancoCuentaBancariaRepositorio;
        public Action<string, string> mensaje;

        public ServicioBancoCuentaBancaria()
        {
            oBancoCuentaBancariaRepositorio = kernel.Get<BancoCuentaBancariaRepositorio>();
        }

        public List<BancoCuentaBancariaModel> GetAllBancoCuentaBancaria()
        {
            return Mapper.Map<List<BancoCuentaBancaria>, List<BancoCuentaBancariaModel>>(oBancoCuentaBancariaRepositorio.GetAllBancoCuentaBancaria());
        }

        public BancoCuentaBancariaModel Agregar(BancoCuentaBancariaModel oBancoCuentaBancariaModel)
        {
            try
            {
                oBancoCuentaBancariaModel.Activo = true;
                oBancoCuentaBancariaModel.UltimaModificacion = DateTime.Now;
                var oModel = Mapper.Map<BancoCuentaBancariaModel, BancoCuentaBancaria>(oBancoCuentaBancariaModel);
                return Mapper.Map<BancoCuentaBancaria, BancoCuentaBancariaModel>(oBancoCuentaBancariaRepositorio.Agregar(oModel));
            }
            catch(Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioBancoCuentaBancaria >> Agregar");
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                throw new NotImplementedException();
            }
         
        }

     
        public BancoCuentaBancariaModel IngresoCuentaBancaria(BancoCuentaBancariaModel bancoCuentaBancariaModel)
        {
          return  Agregar(bancoCuentaBancariaModel);
        }

        public List<BancoCuentaBancariaModel> GetAllMovimientosConciliados(int id)
        {
            return Mapper.Map<List<BancoCuentaBancaria>, List<BancoCuentaBancariaModel>>(oBancoCuentaBancariaRepositorio.GetAllMovimientosConciliados(id));
        }

        public void UpdateNumeroCierreMovimiento(BancoCuentaBancariaModel item)
        {
            try
            {            
                var model = Mapper.Map<BancoCuentaBancariaModel, BancoCuentaBancaria>(item);
                oBancoCuentaBancariaRepositorio.UpdateNumeroCierreMovimiento(model);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema" , "error");
                throw new NotImplementedException(ex.Message);
            }
        }

        public void ConciliarMovimiento(int item)
        {
            try
            {                
                oBancoCuentaBancariaRepositorio.ConciliarMovimiento(item);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                throw new NotImplementedException(ex.Message);
            }
        }

        //5  Impresion de Cta Bria

               public List<BancoCuentaBancariaModel> GetCtaBriaImpresion(int id, int idBanco)
        {
            return Mapper.Map<List<BancoCuentaBancaria>, List<BancoCuentaBancariaModel>>(oBancoCuentaBancariaRepositorio.GetCtaBriaImpresion(id,idBanco));
        }

        //5  Impresion de Cta Bria

        public List<BancoCuentaBancariaModel> GetCtaBriaImpresion0(int id, int idBanco, DateTime fecha)
        {
            return Mapper.Map<List<BancoCuentaBancaria>, List<BancoCuentaBancariaModel>>(oBancoCuentaBancariaRepositorio.GetCtaBriaImpresion0(id, idBanco,fecha));
        }

    }
}
