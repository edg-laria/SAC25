using System;
using System.Data.Entity;
using Datos.ModeloDeDatos;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
  public class IvaVentaRepositorio : RepositorioBase<IvaVenta>
    {
        private SAC_Entities context;

        public IvaVentaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<IvaVenta> GetAllIvaVenta()
        {
            List<IvaVenta> listaIvaVenta = context.IvaVenta.Where(p => p.Activo == true).ToList();
            return listaIvaVenta;
        }

        public IvaVenta Agregar(IvaVenta oIvaVenta)
        {
            return Insertar(oIvaVenta);
        }



    }
}
