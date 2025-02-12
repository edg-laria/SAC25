using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class ClienteDireccionModel
    {



        public int Id { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public string CodigoAfip { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> IdPais { get; set; }
        public Nullable<int> IdProvincia { get; set; }
        public Nullable<int> IdLocalidad { get; set; }
        // public Nullable<int> IdCodigoPostal { get; set; }
        public string IdCodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Cuit { get; set; }
        public string Email { get; set; }
        public string IdPieNota { get; set; }
        public Nullable<int> IdIdioma { get; set; }
        public string LocalA { get; set; }
        public Nullable<int> IdTipoMoneda { get; set; }
        public Nullable<bool> Activo { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> UltimaModificacion { get; set; }



        public ClienteModel Cliente { get; set; }      
        public LocalidadModel Localidad { get; set; }
        public LocalidadModel Localidad1 { get; set; }
        public PaisModel Pais { get; set; }
        public ProvinciaModel Provincia { get; set; }
        public TipoIdiomaModel TipoIdioma { get; set; }
        public TipoMonedaModel TipoMoneda { get; set; }


    }
}
