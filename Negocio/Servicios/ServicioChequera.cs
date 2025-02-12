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
    public class ServicioChequera : ServicioBase
    {
        private ChequeraRepositorio pChequeraRepositorio;

       // public Action<string, string> _mensaje;

        public ServicioChequera()
        {
            pChequeraRepositorio = kernel.Get<ChequeraRepositorio>();
        }

        public List<ChequeraModel> GetAllChequera()
        {
            List<ChequeraModel> listaChequera = Mapper.Map<List<Chequera>, List<ChequeraModel>>(pChequeraRepositorio.GetAllChequera());
            return listaChequera;
        }

        public ChequeraModel obtenerCheque(int idCheque)
        {
            return Mapper.Map<Chequera, ChequeraModel>(pChequeraRepositorio.obtenerCheque(idCheque));
        }

        public ChequeraModel Insertar(ChequeraModel oChequeModel)
        {
            try
            {
                //controlar que no exista 
                Chequera oChequera = pChequeraRepositorio.VerificarCheque(oChequeModel.NumeroCheque);
                if (oChequera != null) //significa que existe
                {
                     _mensaje?.Invoke("El número de cheque ya se encuentra ingresado.", "error");
                    return null;
                }
                else //significa que no existe el dato a ingresar
                {
                    var oModel = Mapper.Map<ChequeraModel, Chequera>(oChequeModel);
                    _mensaje?.Invoke("El cheque se ingresó correctamente", "ok");
                    return Mapper.Map<Chequera, ChequeraModel>(pChequeraRepositorio.Insertar(oModel));
                }
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                throw;
            }



        }

        public ChequeraModel InsertarAjax(ChequeraModel oChequeModel)
        {
                Chequera oChequera = pChequeraRepositorio.VerificarCheque(oChequeModel.NumeroCheque);
                if (oChequera != null) 
                {
                    
                  throw new Exception("El número de cheque ya se encuentra ingresado.");                   
                }
                else 
                {
                    var oModel = Mapper.Map<ChequeraModel, Chequera>(oChequeModel);
                    return Mapper.Map<Chequera, ChequeraModel>(pChequeraRepositorio.Insertar(oModel));
                }
            
           


        }



        public ChequeraModel Actualizar(ChequeraModel oChequeModel)
        {
            try
            {
                var oModel = Mapper.Map<ChequeraModel, Chequera>(oChequeModel);
                return Mapper.Map<Chequera, ChequeraModel>(pChequeraRepositorio.Actualizar(oModel));

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

        public List<ChequeraModel> GetChequePropioPorUsuario(int idUsuario)
        {
            try
            {           
                return Mapper.Map<List<Chequera>, List<ChequeraModel>>(pChequeraRepositorio.GetChequePropioPorUsuario(idUsuario));

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

        public void DeleteChequePropio(int id)
        {
            try
            {             
                pChequeraRepositorio.DeleteChequePropio(id);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");              
            }
        }

        public int GetNroChequePorCta(int id)
        {
            return pChequeraRepositorio.GetNroChequePorCta(id);
        }


        public void ActualizarNumeroCheque(ChequeraModel model)
        {
            pChequeraRepositorio.ActualizarNumeroCheque(Mapper.Map<ChequeraModel,Chequera>(model));
        }

        public ChequeraModel GetChequePropioPorId(int id)
        {
            return Mapper.Map<Chequera,ChequeraModel>(pChequeraRepositorio.GetChequePropioPorId(id));
        }
    }
}

