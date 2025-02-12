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
    public class ServicioTarjetaOperacion : ServicioBase
    {

        private TarjetaOperacionRepositorio oTarjetaRepositorio;


        public ServicioTarjetaOperacion()
        {
            oTarjetaRepositorio = kernel.Get<TarjetaOperacionRepositorio>();
        }


        public TarjetaOperacionModel Insertar(TarjetaOperacionModel oTarjetaOperacionModel)
        {
            var oModel = Mapper.Map<TarjetaOperacionModel, TarjetaOperacion>(oTarjetaOperacionModel);
            //_mensaje?.Invoke("El cheque se ingresó correctamente", "ok");
            return Mapper.Map<TarjetaOperacion, TarjetaOperacionModel>(oTarjetaRepositorio.Insertar(oModel));
        }

        public List<TarjetaOperacionModel> GetTarjetaOperacionGastos(int idTipoTarjeta, DateTime cfechadesde, DateTime cfechahasta)
        {
            return Mapper.Map<List<TarjetaOperacion>, List<TarjetaOperacionModel>>(oTarjetaRepositorio.GetTarjetasOperacionGastos(idTipoTarjeta, cfechadesde, cfechahasta));
        }

       public List<TarjetaOperacionModel> GetTarjetaOperacionGastos(int idTipoTarjeta)
        {
            return Mapper.Map<List<TarjetaOperacion>, List<TarjetaOperacionModel>>(oTarjetaRepositorio.GetTarjetasOperacionGastos(idTipoTarjeta));
        }

        public void ConciliarMovimiento(int id)
        {
            try
            {
                oTarjetaRepositorio.ConciliarMovimiento(id);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}
