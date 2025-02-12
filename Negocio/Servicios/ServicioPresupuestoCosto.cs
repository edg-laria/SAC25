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
   public class ServicioPresupuestoCosto : ServicioBase
    {
        private PresupuestoCostoRepositorio PresupuestoCostoRepositorio;


        public ServicioPresupuestoCosto()
        {
            PresupuestoCostoRepositorio = kernel.Get<PresupuestoCostoRepositorio>();
        }


        public List<PresupuestoCostoModel> GetAllPresupuestoCosto()
        {
            return Mapper.Map<List<PresupuestoCosto>, List<PresupuestoCostoModel>>(PresupuestoCostoRepositorio.GetAllPresupuestoCosto());
        }

        public PresupuestoCostoModel GetAllPresupuestoCosto(string codigo)
        {
            return Mapper.Map<PresupuestoCosto, PresupuestoCostoModel>(PresupuestoCostoRepositorio.GetAllPresupuestoCosto(codigo));
        }

        public PresupuestoCostoModel ActualizarPresupuesto(PresupuestoCostoModel oPresupuestoModel)
        {
            PresupuestoCosto oPresupuesto = Mapper.Map<PresupuestoCostoModel, PresupuestoCosto>(oPresupuestoModel);
            return Mapper.Map<PresupuestoCosto, PresupuestoCostoModel>(PresupuestoCostoRepositorio.ActualizarPresupuesto(oPresupuesto));
        }


    }
}
