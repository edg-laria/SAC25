using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;

namespace SAC.Models
{
    public class ClienteDireccionModelView
    {


        public int Id { get; set; }


        [Display(Name = "Nro Cliente")]
        [Required]
        public Nullable<int> IdCliente { get; set; }

        [Display(Name = "Codigo Afip")]
        public string CodigoAfip { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [Display(Name = "Pais")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del pais no corresponde")]
        public Nullable<int>  IdPais { get; set; }

        [Display(Name = "Provincia")]
        [Required]
        //[Range(1, 1000, ErrorMessage = "El valor del Provincia no corresponde")]
        public Nullable<int> IdProvincia { get; set; }


        [Display(Name = "Localidad")]
        [Required]
        public Nullable<int> IdLocalidad { get; set; }

        [Display(Name = "Codico postal")]
        //[Range(1, 1000, ErrorMessage = "El valor del Telefono no corresponde")]
       // public int IdCodigoPostal { get; set; }
        public string IdCodigoPostal { get; set; }

        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Cuit { get; set; }
        public string Email { get; set; }
        [Display(Name = "Pie de Nota")]
        public string IdPieNota { get; set; }

        [Display(Name = "Idioma")]
        public Nullable<int> IdIdioma { get; set; }
        public string LocalA { get; set; }
        [Display(Name = "Moneda")]
        public Nullable<int> IdTipoMoneda { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }


        //1- comento por exeso bre 10/03/2021 (recursivo)
        //2- lo habilite p/ mostrar el cliente en la direccion
        public  ClienteModelView Cliente { get; set; }      
        //-------------

        public  PieNotaModelView PieNota { get; set; }
        public  TipoClienteModelView TipoCliente { get; set; }
        public  TipoMonedaModelView Moneda { get; set; }
        public  TipoIdiomaModelView Idioma { get; set; }
      
        public  PaisModelView Pais { get; set; }
        public  ProvinciaModelView Provincia { get; set; }
        public  LocalidadModelView Localidad { get; set; }


        public List<ClienteDireccionModelView> ListaDireccion { get; set; }






    }
}