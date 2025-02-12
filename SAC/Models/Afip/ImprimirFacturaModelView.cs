using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models.Afip
{
    public class ImprimirFacturaModelView
    {
        public int Id { get; set; }
        public string OurRef { get; set; }
        public string YourRef { get; set; }
        public string RazonSocial { get; set; }
        public string Domicilio { get; set; }
        public string Atencion { get; set; }
        public string Pais { get; set; }
        public string CondicionIVA { get; set; }

        public string Header { get; set; }
        public List<ItemFactura> Items { get; set; }
        public string Dto { get; set; }
        public string ImporteEnLetra { get; set; }
        public string Nota { get; set; }

        public string QrBase64 { get; set; }

        public string TipoComprobante { get; set; }
        public string PuntoVenta { get; set; }
        public string NumeroComprobante { get; set; }
        public string NombreComprobante { get; set; } 
        public string LetraComprobante { get; set; }
        //"2020-10-13"
        public string FechaEmision { get; set; }
        public string CuitEmisor { get; set; }                
        public string ImporteTotal  { get; set; }
        public string Moneda { get; set; }
        public string Cotizacion { get; set; }
        public string TipoDocumentoReceptor { get; set; }
        public string NumeroDocumentoReceptor { get; set; }
        
        // “A” para comprobante autorizado por CAEA, “E” para comprobante autorizado por CAE
        public string tipoCodAut { get; set; }
        // Código de autorización otorgado por AFIP CAE/CAEA
        public string codAut { get; set; }

        public string FechaVtoCodAut { get; set; }
        public string NotaAfipSuperior { get; set; }
        public string NotaAfipInferior { get; set; }
        
    }

    public class ItemFactura
    {
        public string Nombre { get; set; }
        public decimal? Precio { get; set; }
    }


 }