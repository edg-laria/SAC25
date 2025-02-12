using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;

namespace Negocio.Servicios
{
   public class ServicioFacturaVentaItems : ServicioBase
    {
        private FacturaVentasRepositorio oFacturaVenta;
        private ItemImprRepositorio oItemImpr;

        public new Action<string, string> _mensaje;

        public ServicioFacturaVentaItems()
        {
            oFacturaVenta = kernel.Get<FacturaVentasRepositorio>();
            oItemImpr = kernel.Get<ItemImprRepositorio>();
        }


        public FacturaVentaItemsModel ObtenerDatosFacturaItems(int idCliente, int nroFactura, int idComprobante)
        {
            FacturaVentaItemsModel facturaItem = new FacturaVentaItemsModel();
            try
            {
                //FacturaVentaModel factura = Mapper.Map<FactVenta, FacturaVentaModel>(oFacturaVenta.GetFacturaVentaPorNumero(nroFactura, idComprobante.ToString()));
                //add Edgardo
                var fact = oFacturaVenta.GetFacturaVentaPorNumero(nroFactura, idComprobante.ToString());
                
                // Crear una nueva instancia de FacturaVentaModel  
                FacturaVentaModel factura = new FacturaVentaModel();

                // Asignar manualmente las propiedades que deseas copiar de `fact` a `factura`  
                factura.IdMoneda = fact.IdMoneda;
                factura.Concepto = fact.Concepto;
                factura.Cotiza = fact.Cotiza;
                factura.ORef = fact.ORef;
                factura.YRef = fact.YRef;
                factura.IdDto = fact.IdDto;
                factura.NumeroCobro = fact.Id;
                factura.Saldo = fact.Saldo;
                

                List<ItemImprModel> listaItems = Mapper.Map<List<ItemImpre>, List<ItemImprModel>>(oItemImpr.GetAllItemImpreNroFactura(nroFactura, idComprobante));

                facturaItem.factura = factura;
               facturaItem.items = listaItems;
            
                return facturaItem;
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch(Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                return null;
            }


        }


    }
}
