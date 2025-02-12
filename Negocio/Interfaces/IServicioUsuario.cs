using Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Interfaces
{
  public interface IServicioUsuarios
    {     
        IEnumerable<UsuarioModel> Obtener();
        List<UsuarioModel> GetAllUsuario();
        UsuarioModel Agregar(UsuarioModel usuario);
        UsuarioModel ObtenerPorID(int idUsuario);
        UsuarioModel ObtenerUsuario(string documento, int idSistema);
        bool Obtener(string documento, string password, int idRolInvitado);
        void ActualizarRolDeUsaurio(int idUsuario, int idRol, int idUsuarioLogueado);
        void CambiarPassword(int idUsuario, string password);
        void LogLogin(int idUsuario, string ip);
        RolModel ObtenerRol(int idUsuario);                        
    }
}