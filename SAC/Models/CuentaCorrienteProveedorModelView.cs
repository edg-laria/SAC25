using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace SAC.Models
{
    public class CuentaCorrienteProveedorModelView
    {
        public CuentaCorrienteProveedorModelView()
        {
            Proveedor = new ProveedorModelView();
            Detalles = new List<CuentaCorrienteProveedorDetallesModelView>();
        }

        public ProveedorModelView Proveedor { get; set; }

        public List<CuentaCorrienteProveedorDetallesModelView> Detalles { get; set; }

    }

}
