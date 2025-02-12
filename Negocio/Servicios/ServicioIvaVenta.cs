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
using System.Data.Entity.Validation;

namespace Negocio.Servicios
{
    public class ServicioIvaVenta : ServicioBase
    {

        private IvaVentaRepositorio oIvaVentaRepositorio;
#pragma warning disable CS0108 // 'ServicioIvaVenta._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.
        public Action<string, string> _mensaje;
#pragma warning restore CS0108 // 'ServicioIvaVenta._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.

        public ServicioIvaVenta()
        {
            oIvaVentaRepositorio = kernel.Get<IvaVentaRepositorio>();
        }

        public List<IvaVentaModel> GetAllIvaVenta()
        {
            List<IvaVentaModel> listaIvaVenta = Mapper.Map < List <IvaVenta>, List<IvaVentaModel>>(oIvaVentaRepositorio.GetAllIvaVenta());
            return listaIvaVenta;
        }

        public IvaVentaModel Agregar(IvaVentaModel oIvaVentaModel)
        {
            try
            {
                var oModel = Mapper.Map<IvaVentaModel, IvaVenta>(oIvaVentaModel);
                return Mapper.Map<IvaVenta, IvaVentaModel>(oIvaVentaRepositorio.Agregar(oModel));
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return null;
            }
        }
    }
}
