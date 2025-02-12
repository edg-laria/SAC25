using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class AccionRepositorio : RepositorioBase<Accion>
    {
       private SAC_Entities context;
    
        public AccionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public Accion CreateAccion(Accion accion)
        {
           return  Insertar(accion);
        }

        public List<Accion> GetAccion()
        {
            return context.Accion.Where(acc => acc.Activo == true).OrderBy(acc => acc.Controlador).ToList();
        }

        public Accion GetAccionPorId(int id)
        {           
            return context.Accion.Where(acc => acc.IdAccion == id && acc.Activo == true).FirstOrDefault(); 
        }

      
        public Accion ActualizarAccion(Accion AccionParaActualizar)
        {

            Accion Accion = GetAccionPorId(AccionParaActualizar.IdAccion);
            Accion.Controlador = AccionParaActualizar.Controlador ?? Accion.Controlador;
            Accion.Nombre = AccionParaActualizar.Nombre ?? Accion.Nombre;
            Accion.Descripcion = AccionParaActualizar.Descripcion ?? Accion.Descripcion;
            context.SaveChanges();

            return Accion;
        }
        public Accion DeleteAccion(int IdAccion)
        {

            Accion Accion = GetAccionPorId(IdAccion);
            Accion.Activo = false;
            Accion.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();

            return Accion;
        }
        /// <summary>
        ///    acciones por rol que no esten el el menu
        /// </summary>
     
        public List<AccionPorRol> GetAllAccionPorRol(int idRol)
        { 
            var m = context.MenuSidebar
                    .Include(a => a.Accion)                    
                    .Where(x => x.Activo)
                    .Select(x => x.IdAccion)
                    .ToArray();

            var ListAccionPorRol = context.AccionPorRol
                                    .Include(x => x.Accion)
                                    .Where(x => x.idRol == idRol
                                       && !(m.Contains(x.idAccion))
                                    ).ToList();

            return ListAccionPorRol;

        
        }
        public AccionPorRol GetAccionPorRol(int idRol, int idAccion)
        {
            return (from  apr in context.AccionPorRol 
                    where apr.idRol == idRol && apr.idAccion == idAccion                   
                    select apr).FirstOrDefault();
          
        }

        public List<Accion> GetAccionNoMenu(int idRol)
        {
         

            var listAccion = (from a in context.Accion
                    join m in context.MenuSidebar on a.IdAccion equals (int)m.IdAccion
                    into m_join
                    from m in m_join.DefaultIfEmpty()
                    where
                      a.Activo == true &&
                      m.IdAccion == null
                    select a).ToList();
            return listAccion;
        }
    }
}