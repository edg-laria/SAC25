using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//agregado
using Datos.ModeloDeDatos;

namespace Negocio.Modelos
{
  public  class PagosFacturasModel
    {
     

        public int ProveedorSelec_ { get; set; }
        public decimal cotizacion_ { get; set; }

        public DateTime FechaOperacion_ { get; set; }
        public int Periodo_ { get; set; }

        //Cuenta Corriente segun el Pagos.doc
        public List<CompraFacturaModel> ListaFacturasAPagar_ { get; set; }

        //Resumen del pago segun el Pagos.doc - ver si se usa el modelo puesto o creo uno nuevo
        public List<CompraFacturaModel> LsitaFacturasSeleccionadasPagar_ { get; set; }

        public string ConceptoPago_ { get; set; }

        //medios de pago mostrar
     
        public List<ChequeModel> ListaChequesTerceros { get; set; }
        public List<ChequeraModel> ListaChequesPropios { get; set; }

        public ChequeraModel oChequera { get; set; }

        //seleccion tipo de pago

        public int nroRecibo_ { get; set; }
        public int idCuentaBancariaSeleccionada_ { get; set; }
        public int idCuentaBancariaSelec_ { get; set; }
        public int idTipoMonedaSelec_ { get; set; }
        public int idTarjetaSelec_ { get; set; }

        public string idFacturas { get; set; }
        public string idChequesPropios { get; set; }
        public string idChequesTerceros { get; set; }
        public int idPresupuesto_ { get; set; }
        public int idRetencion { get; set; }

        public decimal montoEfectivo_ { get; set; }
        public decimal montoChequesSeleccionados { get; set; }
        public decimal montoTarjetaSelec_ { get; set; }
        public decimal montoCuentaBancaria_ { get; set; }
        public decimal montoRetencion_ { get; set; }
        public decimal montoTotal_ { get; set; }

        public int idProveedor_ { get; set; }
        public int idUsuario_ { get; set; }

        //retenciones 
        public RetencionModel Retencion_ { get; set; }
        public List<RetencionModel> ListadoRetenciones_ { get; set; }
        public decimal totalRetenciones_ { get; set; }



    }
}
