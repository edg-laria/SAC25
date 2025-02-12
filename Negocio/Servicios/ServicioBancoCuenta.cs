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
   public class ServicioBancoCuenta : ServicioBase
    {
        private BancoCuentaRepositorio oBancoCuentaRepositorio;
        public new Action<string, string> _mensaje;

        public ServicioBancoCuenta()
        {
            oBancoCuentaRepositorio = kernel.Get<BancoCuentaRepositorio>();
        }

        public List<BancoCuentaModel> GetAllCuenta()
        {
            return Mapper.Map<List<BancoCuenta>, List<BancoCuentaModel>>(oBancoCuentaRepositorio.GetAllCuenta());
        }

        public BancoCuentaModel GetCuentaPorId(int id)
        {
            return Mapper.Map<BancoCuenta, BancoCuentaModel>(oBancoCuentaRepositorio.GetCuentaPorId(id));
        }

     public List<BancoCuentaModel> GetBancoPorNombre(string strBanco)
        {

            try
            {
                return Mapper.Map<List<BancoCuenta>, List<BancoCuentaModel>>(oBancoCuentaRepositorio.GetBancoPorNombre(strBanco));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }


        }
        public List<BancoCuentaModel> GetBancoCuentaPorNombre(string strBanco)
        {

            try
            {
                return Mapper.Map<List<BancoCuenta>, List<BancoCuentaModel>>(oBancoCuentaRepositorio.GetBancoCuentaPorNombre(strBanco));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }


        }

        public BancoModel GetBancoPorId(int id)
        {
            try
            {
                return Mapper.Map<Banco, BancoModel>(oBancoCuentaRepositorio.GetBancoPorId(id));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }
        public BancoModel GetBancoPorIdLazy(int id)
        {
            try
            {
                return Mapper.Map<Banco, BancoModel>(oBancoCuentaRepositorio.GetBancoPorIdLazy(id));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public List<BancoCuentaBancariaModel> GetmMovimientosPendientesCuentaBancaria(int idBanco, DateTime fecha)
        {
            try
            {
                return Mapper.Map<List<BancoCuentaBancaria>, List<BancoCuentaBancariaModel>>(oBancoCuentaRepositorio.GetmMovimientosPendientesCuentaBancaria(idBanco, fecha));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public BancoCuentaModel GuardarCuentaBancaria(BancoCuentaModel model)
        {
            try
            {
                BancoCuenta response = oBancoCuentaRepositorio.GuardarCuentaBancaria(Mapper.Map<BancoCuentaModel, BancoCuenta>(model));
                return Mapper.Map<BancoCuenta, BancoCuentaModel>(response);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public List<BancoCuentaModel> GetBancoPorFecha(int idBanco, DateTime fecha)
        {
            try
            {
                return Mapper.Map<List<BancoCuenta>, List<BancoCuentaModel>>(oBancoCuentaRepositorio.GetBancoPorFecha(idBanco, fecha));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public BancoCuentaModel GetBancoCuentaPorId(int idBancoCuenta)
        {
            return Mapper.Map<BancoCuenta, BancoCuentaModel>(oBancoCuentaRepositorio.GetCuentaPorId(idBancoCuenta));
        }

        public BancoCuentaModel CierreDeCuentaBancaria(int id, decimal saldoCierre, DateTime FechaCierre)
        {
            try
            {

                return Mapper.Map<BancoCuenta, BancoCuentaModel>(oBancoCuentaRepositorio.CierreDeCuentaBancaria(id, saldoCierre,FechaCierre));
            }
            catch (Exception ex)
            {
                _mensaje.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public List<BancoModel> GetAllBanco()
        {
            try
            {
                return Mapper.Map<List<Banco>, List<BancoModel>>(oBancoCuentaRepositorio.GetAllBanco());
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public BancoCuentaModel UpdateCuentaBancaria(BancoCuentaModel model)
        {
            try
            {
                BancoCuenta response = oBancoCuentaRepositorio.UpdateCuentaBancaria(Mapper.Map<BancoCuentaModel, BancoCuenta>(model));
                return Mapper.Map<BancoCuenta, BancoCuentaModel>(response);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public BancoCuentaModel DeshabilitarCuentaBancaria(BancoCuentaModel model)
        {
            try
            {
                BancoCuenta response = oBancoCuentaRepositorio.DeshabilitarCuentaBancaria(Mapper.Map<BancoCuentaModel, BancoCuenta>(model));
                return Mapper.Map<BancoCuenta, BancoCuentaModel>(response);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }
    }
}
