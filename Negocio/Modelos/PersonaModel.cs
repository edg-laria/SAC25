using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
    public class PersonaModel
    {

        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Ops!, complete el campo...")]
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Cuil { get; set; }
        [Display(Name = "Telefono: ")]
        public string TelefonoMovil { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoAlternativo { get; set; }
        public string CodigoPostal { get; set; }
        public string Domicilio { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }       
        public Nullable<bool> Activo { get; set; }

    }
}