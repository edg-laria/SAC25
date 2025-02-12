using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class ItemImprRepositorio : RepositorioBase<ItemImpre>
    {

        private SAC_Entities context;

        public ItemImprRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public ItemImpre InsertarItemImpre(ItemImpre ItemImpr)
        {
            ItemImpre a = new ItemImpre();
            try
            {
                return Insertar(ItemImpr);
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return a;
            }

        }

        public List<ItemImpre> GetAllItemImpre()
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.ItemImpre.Where(p => p.Activo == true).ToList();
        }

        public List<ItemImpre> GetAllItemImpreNroFactura(int nroFactura, int idComprobante)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.ItemImpre.Where(p => p.Activo == true && p.Factura == nroFactura && p.IdTipoComprobante == idComprobante).ToList();
        }

        


        public ItemImpre GetItemImprePorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.ItemImpre.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault();
        }
       
        public ItemImpre ObtenerItemImprePorCodigo(string nombreCodigo)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.ItemImpre.Where(p => p.Codigo == nombreCodigo).FirstOrDefault();
        }

        
        public ItemImpre ActualizarItemImpre(ItemImpre ItemImpreParaActualizar)
        {
            ItemImpre ItemImpre = GetItemImprePorId(ItemImpreParaActualizar.Id);

            ItemImpre.Id = ItemImpreParaActualizar.Id;
            ItemImpre.IdTipoComprobante = ItemImpreParaActualizar.IdTipoComprobante;
            ItemImpre.PuntoVenta = ItemImpreParaActualizar.PuntoVenta;
            ItemImpre.Factura = ItemImpreParaActualizar.Factura;
            ItemImpre.Codigo = ItemImpreParaActualizar.Codigo;
            ItemImpre.Descripcion= ItemImpreParaActualizar.Descripcion;
            ItemImpre.Cantidad = ItemImpreParaActualizar.Cantidad;
            ItemImpre.Unidad = ItemImpreParaActualizar.Unidad;
            ItemImpre.Precio = ItemImpreParaActualizar.Precio;
            ItemImpre.Total= ItemImpreParaActualizar.Total;
            ItemImpre.AuxiliarNumero = ItemImpreParaActualizar.AuxiliarNumero;
            ItemImpre.Activo = ItemImpreParaActualizar.Activo;
            ItemImpre.IdUsuario= ItemImpreParaActualizar.IdUsuario;
            ItemImpre.UltimaModificacion = ItemImpreParaActualizar.UltimaModificacion;
            context.SaveChanges();
            return ItemImpre;
        }

        public int EliminarItemImpre(int IdItemImpre)
        {
            ItemImpre ItemImpre = GetItemImprePorId(IdItemImpre);
            ItemImpre.Activo = false;
            // Proveedor.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
            return context.SaveChanges();
        }


    }

}

