using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class BuqueModel
    {
        public int Id { get; set; }
        public string Buque1 { get; set; }
        public string Cliente { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public Nullable<int> NumeroFactura { get; set; }
        public string TipoFactura { get; set; }
        public string ORef { get; set; }
        public string YRef { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public string Puerto { get; set; }
        public string Carpeta { get; set; }
        public string Legajo { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }
    }
}
