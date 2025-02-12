using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class GrupoPresupuestoRepositorio : RepositorioBase<GrupoPresupuesto>
    {
       private SAC_Entities context;
    
        public GrupoPresupuestoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        #region "Metodos de Actualizacion"

        /// <summary>
        ///  Agregar GrupoPresupuesto
        /// </summary>

        public GrupoPresupuesto CreateAccion(GrupoPresupuesto model)
        {
           return  Insertar(model);
        }



        /// <summary>
        ///  Modificar GrupoPresupuesto
        /// </summary>

        public GrupoPresupuesto ActualizarGrupoPresupuesto(GrupoPresupuesto Model)
        {

            GrupoPresupuesto Grupo = GetGrupoGrupoPresupuestoPorId(Model.Id);

            Grupo.Id = Model.Id;
            Grupo.Descripcion = Model.Descripcion;
            Grupo.Observaciones = Model.Observaciones;
            Grupo.IdUsuario = Model.IdUsuario;
            Grupo.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;           
           
            context.SaveChanges();
            return Grupo;





        }



        public int DeleteGrupoPresupuesto(int IdGrupoPresupuesto)
        {
            GrupoPresupuesto Grupo = GetGrupoGrupoPresupuestoPorId(IdGrupoPresupuesto);
            Grupo.Activo = false;
            Grupo.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }


        #endregion




        #region "Metodos de Lectura"

        /// <summary>
        ///  Listado completo de todos Grupos de Presupuesto
        /// </summary>

        public List<GrupoPresupuesto> GetAllGrupoPresupuesto()
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.GrupoPresupuesto.Where(acc => acc.Activo == true).OrderBy(acc => acc.Id).ToList();
        }


        /// <summary>
        /// Obtener Grupo Presupuesto por Id
        /// </summary>


        public GrupoPresupuesto GetGrupoGrupoPresupuestoPorId(int id)
        {           
            return context.GrupoPresupuesto.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }


      



        #endregion






    }
}