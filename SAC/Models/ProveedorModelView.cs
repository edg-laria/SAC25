using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;


namespace SAC.Models
{
    public class ProveedorModelView
    {
        public int Id { get; set; }

        [Display(Name = "Nro Cuit")]
        [Required]
        [StringLength(20, ErrorMessage = "La longitud máxima es 20")]
        public string Cuit { get; set; }

        [Display(Name = "Nombre proveedor")]
        [Required]
        [StringLength(50, ErrorMessage = "La longitud máxima es 50")]
        public string Nombre { get; set; }

        [Display(Name = "Dirección")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        public string Direccion { get; set; }

        [Display(Name = "Pais")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del pais no corresponde")]
        public Nullable<int> IdPais { get; set; }

        [Display(Name = "Provincia")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del Provincia no corresponde")]
        public Nullable<int> IdProvincia { get; set; }


        [Display(Name = "Localidad")]
        [Required]
        public Nullable<int> IdLocalidad { get; set; }


        [Display(Name = "Telefono")]
       // [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
        public string Telefono { get; set; }


        [Display(Name = "Tipo de iva")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
        public int IdTipoIva { get; set; }

        [Display(Name = "Dias factura")]
        public Nullable<int> DiasFactura { get; set; }

        [Display(Name = "Nro imputación")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
        public Nullable<int> IdImputacionProveedor { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        [StringLength(50, ErrorMessage = "La longitud máxima es 50")]
        public string Observaciones { get; set; }

        //[Display(Name = "Saldo Cuenta Corriente")]
        //public Nullable<decimal> SaldoCuentaCorriente { get; set; }

        [Display(Name = "Email")]
        //[Required]
        [EmailAddress(ErrorMessage = "Ingrese un email válido")]
        public string Email { get; set; }

        [Display(Name = "Codico postal")]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
        public string IdCodigoPostal { get; set; }

        [Display(Name = "Tipo proveedor")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
        public Nullable<int> IdTipoProveedor { get; set; }

        [Display(Name = "Imputacion factura")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
        public Nullable<int> IdImputacionFactura { get; set; }

        [Display(Name = "Moneda factura")]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
        public Nullable<int> IdMonedaFactura { get; set; }


        [Display(Name = "Descripcion factura")]
        [StringLength(50, ErrorMessage = "La longitud máxima es 50")]
        public string DescripcionFactura { get; set; }

        [Display(Name = "Tipo moneda")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
        public Nullable<int> IdTipoMoneda { get; set; }

        public int idPresupuesto { get; set; }

        [Display(Name = "Punto Venta")]
        public int UltimoPuntoVenta { get; set; }


        public bool Activo { get; set; }

        public int? IdUsuario { get; set; }

        public DateTime UltimaModificacion { get; set; }

        public List<TipoComprobanteModelView>  ListTipoComprobante { get; set; }


        public Imputacion Imputacion { get; set; }

        public Imputacion Imputacion1 { get; set; }

        public Pais Pais { get; set; }

        public Provincia Provincia { get; set; }

        public TipoIva TipoIva { get; set; }

        public TipoMoneda TipoMoneda { get; set; }

        public TipoProveedor TipoProveedor { get; set; }



    }

}