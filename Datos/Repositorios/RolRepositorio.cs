using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class RolRepositorio : RepositorioBase<Rol>
    {
       private SAC_Entities context;
    
        public RolRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
    
        public Rol CrearRol(Rol rol)
        {
            return Insertar(rol);
        }

        public List<Rol> GetAllRol()
        {
            return context.Rol
                .Include(r => r.AccionPorRol)                
                .OrderBy(acc => acc.Descripcion).ToList();
        }

        public Rol GetRolPorId(int id)
        {           
            return context.Rol.Where(r => r.IdRol == id).FirstOrDefault(); 
        }
      
        public void ActualizarRol(Rol RolParaActualizar)
        {            
            Rol rol = GetRolPorId(RolParaActualizar.IdRol);
            rol.Descripcion = RolParaActualizar.Descripcion ?? rol.Descripcion;
            rol.EsAdministrador = RolParaActualizar.EsAdministrador;
            rol.IdHome = RolParaActualizar.IdHome ?? rol.IdHome;
            if (RolParaActualizar.AccionPorRol.Count() > 0) {
                rol.AccionPorRol = RolParaActualizar.AccionPorRol;
            }
            context.SaveChanges();

        }

        public Rol DeleteAccionPorRol(int idRolPorAccion)
        {                       
            var apr = context.AccionPorRol.Where(r => r.idRolPorAccion == idRolPorAccion).FirstOrDefault();
            var idRol = apr.idRol;
            context.AccionPorRol.Remove(apr);
            context.SaveChanges();
            Rol rol = GetRolPorId(idRol);
            return rol;
        }

        public void InsertarAccionPorRol(AccionPorRol model)
        {
            var existsAccionPorRol = GetAccionPorRol(model);
            if(existsAccionPorRol is null)
            { 
            context.AccionPorRol.Add(model);
            context.SaveChanges();
            }
                       
        }

        public AccionPorRol GetAccionPorRol(AccionPorRol model) {

            return context.AccionPorRol.Where(r => r.idRol == model.idRol && r.idAccion == model.idAccion).FirstOrDefault();
        }

       
    }
}