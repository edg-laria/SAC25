using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
  public class TipoProveedorRepositorio:RepositorioBase<TipoProveedor>
    {

        private SAC_Entities context;

        public TipoProveedorRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public TipoProveedor InsertarTipoProveedor(TipoProveedor TipoProveedor)
        {
            return Insertar(TipoProveedor);
        }

        public List<TipoProveedor> GetAllTipoProveedor()
        {
            return context.TipoProveedor.Where(p => p.Activo == true).ToList();
        }

        public TipoProveedor GetTipoProveedorPorId(int id)
        {
            return context.TipoProveedor.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault();
        }

        public TipoProveedor ObtenerTipoProveedorPorNombre(string nombre)
        {
            return context.TipoProveedor.Where(p => p.Descripcion == nombre).FirstOrDefault();
        }
     

        public TipoProveedor ObtenerTipoProveedorPorNombre(string oNombre, string oCuit, int oId)
        {
            return context.TipoProveedor.Where(p => p.Descripcion == oNombre && p.Id != oId).FirstOrDefault();
        }

        public TipoProveedor ActualizarTipoProveedor(TipoProveedor TipoProveedorParaActualizar)
        {
            TipoProveedor TipoProveedor = GetTipoProveedorPorId(TipoProveedorParaActualizar.Id);
            TipoProveedor.Descripcion = TipoProveedorParaActualizar.Descripcion ?? TipoProveedor.Descripcion;
            context.SaveChanges();
            return TipoProveedor;
        }



        public int DeleteTipoProveedor(int IdTipoProveedor)
        {
            TipoProveedor TipoProveedor = GetTipoProveedorPorId(IdTipoProveedor);
            TipoProveedor.Activo = false;
            // TipoProveedor.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
           return context.SaveChanges();
        }





    }
}
