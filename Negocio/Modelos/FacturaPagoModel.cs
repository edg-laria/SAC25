using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;

namespace Negocio.Modelos
{
  public class FacturaPagoModel
    {

        //listado de facturas seleccionadas para pagar, estas se van a cargar del hidden idfacturas
        public List<CompraFacturaModel> FacturasAPagar { get; set; }

        public decimal TotalAPagar { get; set; }

        //fecha
        public DateTime FechaPago { get; set; }

        //resto
        public decimal Diferencia { get; set; }

        //medios de pago
        public decimal efectivo { get; set; }


        public int idTarjeta { get; set; }
   
        public decimal montoTarjetaSeleccionados { get; set; }


        //estas propiedades son de los hidden para pasar al controler
        public string idFacturas { get; set; }
        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }


        public decimal montoChequesSeleccionados { get; set; }

        public string idCuentasBancarias { get; set; }

        public decimal montoTranferencia { get; set; }

        public int idUsuario { get; set; }

        public int idPresupuesto { get; set; }
        public int idProveedor { get; set; }

        //proveedor consultado
        public ProveedorModel Proveedor { get; set; }

    }
}
