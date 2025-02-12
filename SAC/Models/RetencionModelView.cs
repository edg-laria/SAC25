using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAC.Models
{
    public class RetencionModelView
    {


        public RetencionModelView()
        {
            ListaTipoRetencion = new List<SelectListItem>();
            ListadoProvincias = new List<SelectListItem>();
            ListadoFacturas = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public int NroPago { get; set; }
        public int IdCodigoProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public int NroRecibo { get; set; }
        public int IdProvincia { get; set; }
        public Nullable<int> IdFactVenta { get; set; }
        public Nullable<int> IdCompraFactura { get; set; }
        public int IdTipoRetencion { get; set; }
        public int Periodo { get; set; }
        public string Actividad { get; set; }
        public bool Activo { get; set; }
        public int Idusuario { get; set; }

        public System.DateTime UltimaModificacion { get; set; }

    


        public List<SelectListItem> ListaTipoRetencion { get; set; }
        public List<SelectListItem> ListadoProvincias { get; set; }
        public List<SelectListItem> ListadoFacturas { get; set; }

        //public decimal _totalRetenciones { get; set; }
        ////----------------------

        public CompraFacturaViewModel CompraFactura { get; set; }
        public CobroFacturaModelView VentaFactura { get; set; }
        public ProvinciaModelView Provincia { get; set; }
        public TipoRetencionModelView TipoRetencion { get; set; }

    }
}