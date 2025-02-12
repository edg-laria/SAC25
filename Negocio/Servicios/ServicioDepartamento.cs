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
   public class ServicioDepartamento : ServicioBase
    {
        private DepartamentoRepositorio oDepartamentoRepositorio;
       

        public ServicioDepartamento()
        {
            oDepartamentoRepositorio = kernel.Get<DepartamentoRepositorio>();
        }


        #region "Metodos de Actualizacion"

        /// <summary>
        ///  Agregar Departamento
        /// </summary>
        /// 

        public DepartamentoModel Agregar(DepartamentoModel Omodel)
        {
            try
            {

                Omodel.Activo = true;
                Omodel.UltimaModificacion = DateTime.Now;
                var oModel = Mapper.Map<DepartamentoModel, Departamento>(Omodel);
                    _mensaje?.Invoke("Se Agregó correctamente", "ok");

                return Mapper.Map<Departamento, DepartamentoModel>(oDepartamentoRepositorio.Agregar(oModel));

                
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


        /// <summary>
        ///  Modificar Departamento
        /// </summary>
        /// 


        public DepartamentoModel Actualizar(DepartamentoModel oChequeModel)
        {
            try
            {
                oChequeModel.UltimaModificacion = DateTime.Now;
                var oModel = Mapper.Map<DepartamentoModel, Departamento>(oChequeModel);

                _mensaje?.Invoke("Se Actualizó correctamente", "ok");
                return Mapper.Map<Departamento, DepartamentoModel>(oDepartamentoRepositorio.ActualizarDepartamento(oModel));

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }



        /// <summary>
        ///  Eliminar Departamento
        /// </summary>


        public void Eliminar(int IdDepartamento, int idCliente)
        {
            try
            {



                var retorno = oDepartamentoRepositorio.Eliminar(IdDepartamento,idCliente);
                _mensaje?.Invoke("Se Eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "error");
                throw new Exception();

            }

        }


        #endregion



        #region "Metodos de Lectura"

        /// <summary>
        ///  Listado completo de todos lo Departamentos
        /// </summary>


        public List<DepartamentoModel> GetAllDepartamento()
        {
            List<DepartamentoModel> listaCheque = Mapper.Map<List<Departamento>, List<DepartamentoModel>>(oDepartamentoRepositorio.GetAllDepartamento());

           

            return listaCheque;

        }


        /// <summary>
        ///  Obtener un Departamento por el Id
        /// </summary>

        public DepartamentoModel GetDepartamentoPorId(int IdDepartamento)
        {
            return Mapper.Map<Departamento, DepartamentoModel>(oDepartamentoRepositorio.GetDepartamentoPorId(IdDepartamento));
        }



        #endregion







    }
}
