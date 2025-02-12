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
   public class ServicioPresupuestoItem : ServicioBase
    {
        private PresupuestoItemRepositorio PresupuestoItemRepositorio;

        public ServicioPresupuestoItem()
        {
            PresupuestoItemRepositorio = kernel.Get<PresupuestoItemRepositorio>();
        }

        public PresupuestoItemModel Insertar(PresupuestoItemModel presupuestoModel)
        {
            try
            {
                var presupuestoModel1 = Mapper.Map<PresupuestoItemModel, PresupuestoItem>(presupuestoModel);
                return Mapper.Map<PresupuestoItem,PresupuestoItemModel>( PresupuestoItemRepositorio.Insertar(presupuestoModel1));
            }
           catch(Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioPresupuestoItem>> Insertar");
                return null;
            }

        }
        


    }
}
