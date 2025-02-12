using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class ProveedorModel
    {
        public int Id { get; set; }

        public string Cuit { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public Nullable<int> IdPais { get; set; }

        public Nullable<int> IdProvincia { get; set; }

        public Nullable<int> IdLocalidad { get; set; }

        public string Telefono { get; set; }

        public Nullable<int> IdTipoIva { get; set; }

        public Nullable<int> DiasFactura { get; set; }

        public Nullable<int> IdImputacionProveedor { get; set; }

        public string Observaciones { get; set; }

     //   public Nullable<decimal> SaldoCuentaCorriente { get; set; }

        public string Email { get; set; }

        public Nullable<int> IdCodigoPostal { get; set; }

     //   public Nullable<int> IdAfipRegimen { get; set; }

        public Nullable<int> IdTipoProveedor { get; set; }

        public Nullable<int> IdImputacionFactura { get; set; }

        public Nullable<int> IdMonedaFactura { get; set; }

        public string DescripcionFactura { get; set; }

        public Nullable<int> IdTipoMoneda { get; set; }

        public int idPresupuesto { get; set; }

        public Nullable<int> UltimoPuntoVenta { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }


       // public virtual AfipRegimen AfipRegimen { get; set; }

        //public virtual Imputacion Imputacion { get; set; }

        //public virtual Imputacion Imputacion1 { get; set; }

        //public virtual Pais Pais { get; set; }

        //public virtual Provincia Provincia { get; set; }

       public TipoIva TipoIva { get; set; }

        //public virtual TipoMoneda TipoMoneda { get; set; }

        public virtual TipoProveedor TipoProveedor { get; set; }

    }
}
