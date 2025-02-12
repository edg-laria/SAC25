using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class FacturaVentaItemsModel
    {
        public FacturaVentaModel factura { get; set; }
        public List<ItemImprModel> items { get; set; }


    }
}
