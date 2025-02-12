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
   public class ServicioCheque : ServicioBase
    {
        private ChequeRepositorio pChequeRepositorio;
        public new Action<string, string> _mensaje;

        public ServicioCheque()
        {
            pChequeRepositorio = kernel.Get<ChequeRepositorio>();
        }


        public List<ChequeModel> GetAllCheque()
        {
            List<ChequeModel> listaCheque = Mapper.Map<List<Cheque>, List<ChequeModel>>(pChequeRepositorio.GetAllCheque());
            return listaCheque;
        }

        public ChequeModel obtenerCheque(int idCheque)
        {
            return Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.obtenerCheque(idCheque));
        }


        public ChequeModel Agregar(ChequeModel oChequeModel)
        {
            try
            {
                var oModel = Mapper.Map<ChequeModel, Cheque>(oChequeModel);
                return Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.Agregar(oModel));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


        public ChequeModel Actualizar(ChequeModel oChequeModel)
        {
            try
            {
                var oModel = Mapper.Map<ChequeModel, Cheque>(oChequeModel);
                return Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.Actualizar(oModel));

            }
            catch (Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioCheque>> Actualizar");
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

        public List<ChequeModel> GetAllChequePorCliente(int cIdCliente, DateTime cfechadesde, DateTime cfechahasta)
        {
            List<ChequeModel> listaCheque = Mapper.Map<List<Cheque>, List<ChequeModel>>(pChequeRepositorio.obtenerChequePorCliente(cIdCliente, cfechadesde, cfechahasta));
            return listaCheque;
        }

        public List<ChequeModel> GetAllChequePorBanco(int cIdbanco, DateTime cfechadesde, DateTime cfechahasta)
        {


            List<ChequeModel> listaCheque = Mapper.Map<List<Cheque>, List<ChequeModel>>(pChequeRepositorio.obtenerChequePorBanco(cIdbanco, cfechadesde, cfechahasta));


            return listaCheque;




        }

        public List<ChequeModel> BuscarCheque(int idCliente, int idbanco, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<ChequeModel> listaCheque = Mapper.Map<List<Cheque>, List<ChequeModel>>(pChequeRepositorio.BuscarCheque(idCliente,idbanco, fechaDesde, fechaHasta));
            return listaCheque;
        }

        public ChequeModel ExisteCheque(ChequeModel chequeModel)
        {
            try
            {
                
                return Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.ExisteCheque(Mapper.Map<ChequeModel, Cheque>(chequeModel)));

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

        public ChequeModel IngresarChequeCliente(ChequeModel chequeModel)
        {
            try
            {
                return Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.Agregar(Mapper.Map<ChequeModel, Cheque>(chequeModel)));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

        public void DeleteCheque(int idCheque, int idUsuario)
        {
            try
            {
                ChequeModel cheque = obtenerCheque(idCheque);
                if (cheque != null)
                {
                    cheque.Activo = false;
                    cheque.IdUsuario = idUsuario;
                    cheque.UltimaModificacion = DateTime.Now;
                    Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.Actualizar(Mapper.Map<ChequeModel, Cheque>(cheque)));                   
                }                  
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");                
            }
        }
    }
}
