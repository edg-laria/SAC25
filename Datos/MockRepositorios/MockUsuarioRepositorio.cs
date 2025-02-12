using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;

namespace Datos.MockRepositorios
{
    public class MockUsuarioRepositorio : Repositorios.RepositorioBase<Usuario>, Interfaces.IUsuarioRepositorio
    {
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Rol> roles = new List<Rol>();

        public MockUsuarioRepositorio(Agenda_Entities contextoDeDatos) : base(contextoDeDatos)
        {
        }

        public new IQueryable<Usuario> Obtener()
        {
            return this.usuarios.AsQueryable();
        }

        private bool disposedValue; // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        // Protected Overrides Sub Finalize()
        // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        // Dispose(False)
        // MyBase.Finalize()
        // End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public new Usuario Insertar(Usuario entidad)
        {
            usuarios.Add(entidad);
            entidad.idUsuario = usuarios.Count() + 1;
            return entidad;
        }

        public Usuario ObtenerPorDocumento(int documento, int idTipoDocumento)
        {
            Accion accion = new Accion() { controlador = "home", idSistema = 1, nombre = "index" };
            Rol rol = new Rol() { AccionPorRol = new List<AccionPorRol>() };

            var accionPorRol = new AccionPorRol() { Accion = accion, Rol = rol };
            rol.AccionPorRol.Add(accionPorRol);

            Usuario usuario = new Usuario() { idRol = 1 };
            return usuario;
        }

        public void AgregarAccion(Accion accion)
        {
        }


        public Rol AgregarRol(Rol rol)
        {
            roles.Add(rol);
            return rol;
        }

        public Rol ObtenerRol(int idRol)
        {
            return roles.Where(rol => rol.idRol == idRol).FirstOrDefault();
        }

        //public new bool ObtenerUsuario(int documento, int idTipoDocumento, string password, int idRolInvitado)
        //{
        //    return usuarios.Exists(usuario => password == usuario.password
        //                               & usuario.Persona.documento == documento
        //                               & usuario.Persona.idTipoDocumento == idTipoDocumento);
        //}

        public List<Persona> BuscarPersona(string DNI, string apellido, string nombre, string sexo, string provincia, string localidad, string fechaNacimiento)
        {
            throw new NotImplementedException();
        }

        public Persona obtenerPersonaPorId(int id)
        {
        }

        public Usuario ObtenerPorID(int idUsuario)
        {
        }


        public List<Role> ObtenerRoles()
        {
            throw new NotImplementedException();
        }

        public object ObtenerMenuUsuario(int idUsuario, MenuItemModel[] menuItems)
        {
            throw new NotImplementedException();
        }

        public void CambiarPassword(int idUsuario, string password)
        {
        }

        public List<Unidad> ObtenerUnidades()
        {
        }


        public Persona ObtenerPersonaPorIdPaciente(int idPaciete)
        {
        }

        public Persona ObtenerPersonaPorDocumento(int documento)
        {
        }

        public int ObtenerIdFuerzaPorDenominacion(string denominacion)
        {
            return 1;
        }

        public Localidad ObtenerInformacionLocalidadPorNombre(string nombreLocalidad)
        {
        }

        public void ActualizarRolDeUsaurio(int idUsuario, int idRol, int idUsuarioLogueado)
        {
            throw new NotImplementedException();
        }
        // @LL
        public Persona ActualizaPersona(Persona personaParaGuardar, int idRol, string password)
        {
            throw new NotImplementedException();
        }
        public Persona ActualizaPersona(Persona personaParaGuardar)
        {
            throw new NotImplementedException();
        }
        public Unidad ObtenerUnidadPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void ActualizarUnidadDeUsaurio(int idPersona, int idUnidad, int idUsuarioLogueado)
        {
            throw new NotImplementedException();
        }

        public Usuario PopularUsuario(int idPersona, int idRol, string password)
        {
            throw new NotImplementedException();
        }

        public Persona CrearPersona(Persona personaParaGuardar, int idRol, string password)
        {
            throw new NotImplementedException();
        }

        public object ObtenerInforme(int idUsuario, string ip)
        {
            throw new NotImplementedException();
        }
    }

}
