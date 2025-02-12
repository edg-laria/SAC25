using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;


namespace Negocio.Servicios
{
  public class ServicioCotiza : ServicioBase
    {
        private CotizaRepositorio oCotizaRepositorio;
        public new Action<string, string> _mensaje;

        public ServicioCotiza()
        {
            oCotizaRepositorio = kernel.Get<CotizaRepositorio>();
        }

        public CotizaModel obtenerCheque(DateTime fecha)
        {
            return Mapper.Map<Cotiza, CotizaModel>(oCotizaRepositorio.obtenerCotiza(fecha));
        }

        public CotizaModel Agregar(CotizaModel oCotizaModel)
        {
            try
            {
                var oModel = Mapper.Map<CotizaModel, Cotiza>(oCotizaModel);
                return Mapper.Map<Cotiza, CotizaModel>(oCotizaRepositorio.Agregar(oModel));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


    }
}
