using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
using Entidad.Modelos;
namespace Datos.Interfaces
{
   public interface IUsuarioRepositorio : IRepositorio<Usuario>, IDisposable
    {
        Usuario ObtenerUsuarioPorUserNameEmail(string documento);
        Usuario ObtenerPorDocumento(string documento);
        Usuario ObtenerPorID(int idUsuario);
        List<Usuario> GetAllUsuario();
        bool Obtener(string documento, string password, int idRolInvitado);
        List<MenuItemModel> ObtenerMenuUsuario(int idUsuario, MenuItemModel[] menuItems);
        
        List<MenuSidebar> ObtenerMenuUsuario(int idUsuario);

        void CambiarPassword(int idUsuario, string password);
        void ActualizarRolDeUsaurio(int idUsuario, int idRol, int idUsuarioLogueado);
        object logLogin(int idUsuario, string IP);
        Rol ObtenerRol(int idUsuario);
        void CreateUsuario(Usuario usuario);        
        void UpdateUsuario(Usuario usuario);




        //  List<Persona> BuscarPersona(string DNI, string apellido, string nombre, string sexo, string provincia, string localidad, string fechaNacimiento);
        // Persona obtenerPersonaPorId(int id);
        //  void AgregarAccion(Accion accion);
        //  Rol AgregarRol(Rol rol);                


        // List<Rol> ObtenerRoles();
        //void ActualizarUnidadDeUsaurio(int idPersona, int idUnidad, int idUsuarioLogueado);
        //List<Unidad> ObtenerUnidades();
        //Usuario PopularUsuario(int idPersona, int idRol, string password);
        //Persona CrearPersona(Persona personaParaGuardar, int idRol, string password);
        //Persona ActualizaPersona(Persona personaParaGuardar, int idRol, string password);
        //Persona ActualizaPersona(Persona personaParaGuardar);
        //Persona ObtenerPersonaPorIdPaciente(int idPaciete);
        //Persona ObtenerPersonaPorDocumento(int documento);
        //int ObtenerIdFuerzaPorDenominacion(string denominacion);
        //Localidad ObtenerInformacionLocalidadPorNombre(string nombreLocalidad);
        // Unidad ObtenerUnidadPorId(int id);


    }


}