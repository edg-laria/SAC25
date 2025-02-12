using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;

namespace SAC.Models
{
    public class ClienteModelView
    {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Tipo Iva")]
        public int IdTipoiva { get; set; }
        public string Cuit { get; set; }
        [Display(Name = "Dias de Factura")]
        public int DiasFactura { get; set; }

        [Display(Name = "Imputacion")]
        public int IdImputacion { get; set; }
        public string Observaciones { get; set; }
        public string Email { get; set; }
        public int IdXml { get; set; }
        [Display(Name = "Pie de Nota")]
        public int IdPieNota { get; set; }
        [Display(Name = "Idioma")]
        public int IdIdioma { get; set; }
        [Display(Name = "Tipo Cliente")]
        public int IdTipoCliente { get; set; }
         public bool Visible { get; set; }
        public Nullable<int> IdNotaPieB { get; set; }
        [Display(Name = "Tipo Moneda")]
        public int IdTipoMoneda { get; set; }
        [Display(Name = "Grupo Presupuesto")]
        public int IdGrupoPresupuesto { get; set; }
        public bool MiPyme { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime UltimaModificacion { get; set; }

        public virtual GrupoPresupuestoModelView GrupoPresupuesto { get; set; }
        public virtual PieNotaModelView PieNota { get; set; }
        public virtual TipoClienteModelView TipoCliente { get; set; }
        public virtual TipoMonedaModelView TipoMoneda { get; set; }
        public virtual ICollection<ClienteDireccionModelView> ClienteDireccion { get; set; }

        public virtual ICollection<FacturaVentaModelView> FactVenta { get; set; }

        public List<ClienteModelView> ListaCliente { get; set; }

        public int CidTipoCliente { get; set; }
        public bool CVisible { get; set; }

        public int IdCliente { get; set; }

        public int CNombreCliente { get; set; }

        //agrego listaComprobantes
        public List<TipoComprobanteModelView> ListaComprobantes { get; set; }

        public List<SelectListItem> ListaComprobantesDrop { get; set; }



        // Variableas para Consultas Resumen

        // 1. Cta Cte Detalle

        public List<CobroFacturaModelView> ListaFacturas{ get; set; }//

        //

        public decimal TotalPesos { get; set; }
        public decimal TotalDolares { get; set; }
        public DateTime FechaUltimoMov { get; set; }


        public int IdBancoCuenta{ get; set; }





    }
}