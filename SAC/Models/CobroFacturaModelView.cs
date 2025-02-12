using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;
using Negocio.Modelos;
using System.Text;
using Newtonsoft.Json;

namespace SAC.Models
{
    public class CobroFacturaModelView
    {

     


        public int Id { get; set; }
        public int IdTipoComprobante { get; set; }
        public int NumeroFactura { get; set; }
        public string Codigo { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal Total { get; set; }
        public decimal Parcial { get; set; }
        public decimal Saldo { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime Vencimiento { get; set; }
        public string Tipo { get; set; }
        public string Baja { get; set; }
        public string Impre { get; set; }
        public Nullable<System.DateTime> FechaCobro { get; set; }
        public string TipoIva { get; set; }
        public string Concepto { get; set; }
        public string Marca { get; set; }
        public string Condicion { get; set; }
        public int IdProvincia { get; set; }
        public int IdPais { get; set; }
        public int IdImputacion { get; set; }
        public int NumeroCobro { get; set; }
        public decimal Cotiza { get; set; }
        public int IdMoneda { get; set; }
        public string ORef { get; set; }
        public string YRef { get; set; }
        public decimal Gasto { get; set; }
        public string Descuento { get; set; }
        public decimal CotizaP { get; set; }
        public string TipoFac { get; set; }
        public int CodigoDiario { get; set; }
        public string Recibo { get; set; }
        public int Periodo { get; set; }
        public string NumeroTra { get; set; }
        public double Combria { get; set; }
        public double IngBr { get; set; }
        public string NotaBanco { get; set; }
        public string Anula { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        //--------------
        public decimal cobro { get; set; }
        public decimal aplicacion { get; set; }
        public decimal saldoCobro { get; set; }

        //--------------
        public TipoComprobanteVentaModel TipoComprobanteVenta { get; set; }
        public List<CobroFacturaModoModel> ModoCobro { get; set; }
        public List<ItemImprModel> ItemImpre { get; set; }
        public List<RetencionModel> Retencion { get; set; }
        public  TipoMoneda TipoMoneda { get; set; }


        public  FacturaElectronicaModel FacturaElectronica { get; set; }
        public Nullable<int> IdBancoCuenta { get; set; }

        public string paramQR { get; set; }

    }

}