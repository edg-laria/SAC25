using System;

using System.Linq;

using Datos.ModeloDeDatos;


namespace Negocio.Modelos
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool EsAdministrador { get; set; }
        public string Password { get; set; }
        public Nullable<int> IdPersona { get; set; }
        public Nullable<int> IdRol { get; set; }
        public bool Activo { get; set; }
        public Nullable<System.DateTime> Creado { get; set; }
        public Nullable<System.DateTime> Actualizado { get; set; }
        public string Documento { get; set; }
        public PersonaModel Persona { get; set; }
        public RolModel Rol { get; set; }
        public RolModel rolModel = new RolModel();
   
        public UsuarioModel()
        {

        }
     
        //public UsuarioModel(string nombreDeUsuario, string apellidoDeusuario)
        //{

        //    this.Nombre = nombreDeUsuario;
        //    this.Apellido = apellidoDeusuario;
        //    this.EsAdministrador = false;

        //}

        public void CargarRoles(RolModel rol)
        {
            this.rolModel = rol;
        }
            
        public bool TienePermisos(string controlador, string accion)
        {
          //  return true;
            if ((!this.rolModel.Acciones.ContainsKey(controlador)))
            { return false; }

            return (rolModel.Acciones[controlador].Where(accionDeRol => accionDeRol == accion).Count() != 0);
        }

        public int ObtenerRol()
        {
            return rolModel.IdRol;
        }
    }
}
