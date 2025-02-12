using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
  public class ClienteDireccionRepositorio : RepositorioBase<ClienteDireccion>
    {
        private SAC_Entities context;

        public ClienteDireccionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        #region "Metodos de Actualizacion"

        /// <summary>
        ///  Agregar Direccion de un Cliente
        /// </summary>

        public ClienteDireccion Agregar(ClienteDireccion oClienteDireccion)
        {
            return Insertar(oClienteDireccion);
        }


        /// <summary>
        ///  Actualizar Direccion del Cliente
        /// </summary>
        public ClienteDireccion ActualizarDireccion(ClienteDireccion oDireccion)
        {
            ClienteDireccion nDireccion = GetObtenerDireccion(oDireccion.Id);

            nDireccion.Id = oDireccion.Id;
            nDireccion.IdCliente = oDireccion.IdCliente;
            nDireccion.CodigoAfip = oDireccion.CodigoAfip;
            nDireccion.Nombre = oDireccion.Nombre;


            nDireccion.Direccion = oDireccion.Direccion;
            nDireccion.IdPais = oDireccion.IdPais;
            nDireccion.IdProvincia = oDireccion.IdProvincia;
            nDireccion.IdLocalidad = oDireccion.IdLocalidad;
            nDireccion.IdCodigoPostal = oDireccion.IdCodigoPostal;
            nDireccion.Telefono = oDireccion.Telefono;
            nDireccion.Fax = oDireccion.Fax;
            nDireccion.Cuit = oDireccion.Cuit;

            nDireccion.Email = oDireccion.Email;
           
            nDireccion.IdPieNota = oDireccion.IdPieNota;
            nDireccion.IdIdioma = oDireccion.IdIdioma;
            nDireccion.LocalA = oDireccion.LocalA;
            nDireccion.IdTipoMoneda = oDireccion.IdTipoMoneda;





            nDireccion.Activo = true;
            nDireccion.IdUsuario = oDireccion.IdUsuario;
            nDireccion.UltimaModificacion = oDireccion.UltimaModificacion;
            context.SaveChanges();
            return nDireccion;
        }

     

        /// <summary>
        ///  Ocultar un Cliente
        /// </summary>
        public int DeleteDireccion(int IdDireccion)
        {
            ClienteDireccion nDireccion = GetObtenerDireccion(IdDireccion);


            nDireccion.Activo = false;
          
            nDireccion.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;
        }


      

        #endregion

        /// <summary>
        /// Lista de Todas la direccion por el Id del cliente
        /// </summary>

        public List<ClienteDireccion> GetDireccionPorcliente(int idcliente)
        {
           
            context.Configuration.LazyLoadingEnabled = false;
            List<ClienteDireccion> listaCliente = context.ClienteDireccion
            .Include("Pais")
            .Include("Provincia")
            .Include("Localidad")
            .Where(p => p.Activo == true && p.IdCliente == idcliente).ToList();
            return listaCliente;
        }

        /// <summary>
        ///  Obtener una direccion por el Id
        /// </summary>
        public ClienteDireccion GetObtenerDireccion(int IdDireccion)
        {
            context.Configuration.LazyLoadingEnabled = false;

            ClienteDireccion dir = context.ClienteDireccion
            .Include("Pais")
            .Include("Provincia")
            .Include("Localidad")
            .Where(p => p.Id == IdDireccion).First();
            return dir;
        }


        /// <summary>
        /// Lista de Todas la Direcciones
        /// </summary>

        public List<ClienteDireccion> GetAllClienteDireccion()
        {
           context.Configuration.LazyLoadingEnabled = false;

            List<ClienteDireccion> listaCliente = context.ClienteDireccion
                .Include("Pais")
                .Include("Provincia")
                .Include("TipoIdioma")
                .Include("TipoMoneda")
                .Where(p => p.Activo == true).ToList();

            return listaCliente;
        }


    }
}
