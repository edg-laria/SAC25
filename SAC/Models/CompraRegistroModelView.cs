using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Negocio.Modelos;
using Datos.ModeloDeDatos;
using System.ComponentModel;

namespace SAC.Models
{
    public class CompraRegistroModelView
    {
        public CompraRegistroModelView()
        {
            Proveedor = new ProveedorModelView();
            DetalleFacturas = new List<CompraRegistroDetalleModelView>();
        }

        public ProveedorModelView Proveedor { get; set; }
        public List<CompraRegistroDetalleModelView> DetalleFacturas { get; set; }





    }
}