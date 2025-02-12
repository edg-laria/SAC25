using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class BancoModelView
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Recibo { get; set; }
        public Nullable<int> IdProveedor { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.DateTime> FechaC { get; set; }
        public Nullable<decimal> Importe { get; set; }
        public string NumeroCheque { get; set; }
        public int IdImputacion { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }

        public ProveedorModel Proveedor { get; set; }
        public ICollection<BancoCuentaModel> BancoCuenta { get; set; }
    }


}
