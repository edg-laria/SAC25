using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
  public class DepartamentoRepositorio : RepositorioBase<Departamento>
    {
        private SAC_Entities context;

        public DepartamentoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

         #region "Metodos de Actualizacion"

        /// <summary>
        ///  Agregar Departamento
        /// </summary>

        public Departamento Agregar(Departamento oDepartamento)

        {

            try
            {

                return Insertar(oDepartamento);
            }


            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

            }



            catch (Exception ex)
            {
               
                        System.Diagnostics.Debug.WriteLine("Property: " + ex.InnerException + " Error: " + ex.Message);
               

            }

            return Insertar(oDepartamento);

        }

        /// <summary>
        ///  Actualizar Departamento
        /// </summary>
        public Departamento ActualizarDepartamento(Departamento oDepartamento)
        {
            Departamento nDepartamento = GetDepartamentoPorId (oDepartamento.Id);
            nDepartamento.Id = oDepartamento.Id;

            nDepartamento.Descripcion = oDepartamento.Descripcion;
            nDepartamento.Observaciones= oDepartamento.Observaciones;
            nDepartamento.TotalMes = oDepartamento.TotalMes;
            nDepartamento.TotalAnio = oDepartamento.TotalAnio;


            //nDepartamento.Activo = true;

            nDepartamento.IdUsuario = oDepartamento.IdUsuario;
            nDepartamento.UltimaModificacion = oDepartamento.UltimaModificacion;


            try
            {

                context.SaveChanges();


            }



            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }

            }

            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Property: " + ex.InnerException + " Error: " + ex.Message);


            }







            return nDepartamento;
        }

       

      




        public object Eliminar(int IdDepartamento,int IdCliente)
        {
            Departamento ODepartamento = GetDepartamentoPorId(IdDepartamento);
            ODepartamento.Activo = false;
            ODepartamento.IdUsuario = IdCliente;

            ODepartamento.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;
        }

      

        #endregion


        #region "Metodos de Lectura"

        /// <summary>
        ///  Listado completo de todos lo Clientes
        /// </summary>

        public List<Departamento> GetAllDepartamento()
        {
            List<Departamento> listaCliente = context.Departamento
            
                .Where(p => p.Activo == true).ToList();
       
            return listaCliente;



          

        }


       


        /// <summary>
        ///  Obtener un Departamento por el Id
        /// </summary>
        public Departamento GetDepartamentoPorId(int IdDepartamento)
        {
            return context.Departamento
              
                .Where(p => p.Id == IdDepartamento).First();
        }


        /// <summary>
        ///  Obtener Listado de Departamento Por Nombre y tipo de cliente
        /// </summary>

     


      

      


        /// <summary>
        ///  obtener el Departamento por el Nombre o Codigo
        /// </summary>
        public List<Departamento> GetDepartamentoPorNombre(string strDepartamento)



        {

            List<Departamento> ListaDepartamento = context.Departamento
                           .Where(p => p.Activo == true && p.Descripcion.Contains(strDepartamento)).ToList();

            return ListaDepartamento;

           
        }


      

        

    #endregion


}
}
