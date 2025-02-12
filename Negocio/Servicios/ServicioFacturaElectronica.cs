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
   public class ServicioFacturaElectronica : ServicioBase
    {
        private FacturaElectronicaRepositorio oFacturaElectronicaRepositorio;
#pragma warning disable CS0108 // 'ServicioFacturaElectronica._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.
        public Action<string, string> _mensaje;
#pragma warning restore CS0108 // 'ServicioFacturaElectronica._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.

        public ServicioFacturaElectronica()
        {
            oFacturaElectronicaRepositorio = kernel.Get<FacturaElectronicaRepositorio>();
        }

        public List<FacturaElectronicaModel> GetAllFacturasElectonicas()
        {
            return Mapper.Map<List<FacturaElectronica>, List<FacturaElectronicaModel>>(oFacturaElectronicaRepositorio.GetAllFacturasElectonicas());
        }

        public FacturaElectronicaModel GetFacturasElectonicasPorId(int tipoComprobante, int idPuntoVenta, int nroCbte)
        {
            return Mapper.Map<FacturaElectronica, FacturaElectronicaModel>(oFacturaElectronicaRepositorio.GetFacturasElectonicasPorId(tipoComprobante, idPuntoVenta, nroCbte));
        }

        public FacturaElectronicaModel Agregar(FacturaElectronicaModel oFacturaElectronica)
        {
            var oModel = Mapper.Map<FacturaElectronicaModel, FacturaElectronica>(oFacturaElectronica);
            return Mapper.Map<FacturaElectronica, FacturaElectronicaModel>(oFacturaElectronicaRepositorio.Agregar(oModel));
        }
        /// <summary>
        /// metodo que se utiliza para actualizar la fatura luego de ir a AFIP 
        /// </summary>
        /// <param name="oFacturaElectronica"></param>
        /// <returns></returns>
        public FacturaElectronicaModel Actualizar(FacturaElectronicaModel oFacturaElectronica)
        {
            var oModel = Mapper.Map<FacturaElectronicaModel, FacturaElectronica>(oFacturaElectronica);
            return Mapper.Map<FacturaElectronica, FacturaElectronicaModel>(oFacturaElectronicaRepositorio.Actualizar(oModel));
        }

    }
}
