using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
  public class PieNotaRepositorio : RepositorioBase<PieNota>
    {
        private SAC_Entities context;

        public PieNotaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        #region "Metodos de Actualizacion"

        /// <summary>
        ///  Agregar Cliente
        /// </summary>

        public PieNota Agregar(PieNota oPieNota)

        {

            try
            {
                return Insertar(oPieNota);
            }

            catch (Exception ex)
            {               
                throw new Exception(ex.Message);
               }

        }

        /// <summary>
        ///  Actualizar PieNota
        /// </summary>
        public PieNota ActualizarPieNota(PieNota oPieNota)
        {

           

                PieNota nPieNota = GetPieNotaPorId(oPieNota.Id);
                nPieNota.Id = oPieNota.Id;
                nPieNota.Nota = oPieNota.Nota;
                nPieNota.Descripcion = oPieNota.Descripcion;
                nPieNota.IdBancoCuenta = oPieNota.IdBancoCuenta;
                nPieNota.Cliente = oPieNota.Cliente;
                nPieNota.IdUsuario = oPieNota.IdUsuario;
                nPieNota.UltimaModificacion = oPieNota.UltimaModificacion;

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


            return nPieNota;

        }
        /// <summary>
        ///  Eliminar por Id en la Tabla PieNota
        /// </summary>


        public int DeletePieNota(int IdPieNota)
        {
            PieNota OPieNota = GetPieNotaPorId(IdPieNota);
            OPieNota.Activo = false;

            OPieNota.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }



        public object HabilitarPieNota(int idPieNota)
        {
            PieNota OPieNota = GetPieNotaPorId(idPieNota);
            OPieNota.Visible = true;

            OPieNota.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;
        }

        public object BloquearPieNota(int IdPieNota)
        {

            PieNota OPieNota = GetPieNotaPorId(IdPieNota);
            OPieNota.Visible = false;

            OPieNota.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }




        #endregion


        #region "Metodos de Lectura"

        /// <summary>
        ///  Listado completo de todos los Pie de Nota
        /// </summary>

        public List<PieNota> GetAllPieNota()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<PieNota> ListaPieNota= context.PieNota.Where(p => p.Activo == true).ToList();
       
            return ListaPieNota;



          

        }


        /// <summary>
        ///  Obtener un PieNota por el Id
        /// </summary>
        public PieNota GetPieNotaPorId(int IdPieNota)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.PieNota.Include("BancoCuenta").Where(p => p.Id == IdPieNota).First();
        }

         public PieNota GetPieNotaPorCodigo(string codigo)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.PieNota.Include("BancoCuenta").Where(p => p.BancoCuenta.Codigo.Contains(codigo)).First();
        }
        #endregion


    }
}
