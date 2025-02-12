using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
    public class RetencionModel
    {

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

 
        public CompraFacturaModel CompraFactura { get; set; }
        public  CobroFacturaModel FactVenta { get; set; }
        public ProvinciaModel Provincia { get; set; }
        //public TipoRetencionModel _TipoRetencion { get; set; }
        public TipoRetencionModel TipoRetencion { get; set; }
   
   //public CompraFacturaModel CompraFactura { get; set; }
        //public ProvinciaModel Provincia { get; set; }


    }
}
