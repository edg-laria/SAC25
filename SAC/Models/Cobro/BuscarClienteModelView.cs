using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;


namespace SAC.Models.Cobro
{
    public class BuscarClienteModelView
    {
        
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }

        public ProveedorModelView Proveedor { get; set; }



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
        //[StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        public string Direccion { get; set; }

        [Display(Name = "Pais")]
        [Required]
        public Nullable<int> IdPais { get; set; }

        [Display(Name = "Provincia")]
        [Required]
         public Nullable<int> IdProvincia { get; set; }


        [Display(Name = "Localidad")]
        [Required]
        public Nullable<int> IdLocalidad { get; set; }


        [Display(Name = "Telefono")]
        [Required]
         public string Telefono { get; set; }


        [Display(Name = "Tipo de iva")]
        [Required]
        public int IdTipoIva { get; set; }

        [Display(Name = "Dias factura")]
        public Nullable<int> DiasFactura { get; set; }

        [Display(Name = "Nro imputación")]
        [Required]       
        public Nullable<int> IdImputacionProveedor { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        [StringLength(50, ErrorMessage = "La longitud máxima es 50")]
        public string Observaciones { get; set; }
       
        [Display(Name = "Email")]
        [Required]
        [EmailAddress(ErrorMessage = "Ingrese un email válido")]
        public string Email { get; set; }

        [Display(Name = "Codico postal")]
        public int IdCodigoPostal { get; set; }

        [Display(Name = "Tipo proveedor")]
        [Required]
        
        public Nullable<int> IdTipoProveedor { get; set; }

        [Display(Name = "Imputacion factura")]
        [Required]
        
        public Nullable<int> IdImputacionFactura { get; set; }

        [Display(Name = "Moneda factura")]
      
        public Nullable<int> IdMonedaFactura { get; set; }


        [Display(Name = "Descripcion factura")]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        public string DescripcionFactura { get; set; }

        [Display(Name = "Tipo moneda")]
        [Required]
       
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