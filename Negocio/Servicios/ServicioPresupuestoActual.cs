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
   public class ServicioPresupuestoActual : ServicioBase
    {

        private PresupuestoActualRepositorio oPresupuestoActualRepositorio;
#pragma warning disable CS0108 // 'ServicioPresupuestoActual._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.
        public Action<string, string> _mensaje;
#pragma warning restore CS0108 // 'ServicioPresupuestoActual._mensaje' oculta el miembro heredado 'ServicioBase._mensaje'. Use la palabra clave new si su intención era ocultarlo.

        public ServicioPresupuestoActual()
        {
            oPresupuestoActualRepositorio = kernel.Get<PresupuestoActualRepositorio>();
        }
        public List<PresupuestoActualModel> GetPresupuestoCliente()
        {
            try
            {
                return Mapper.Map<List<PrespuestoActual>, List<PresupuestoActualModel>>(oPresupuestoActualRepositorio.GetPresupuestoCliente());

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }
        public List<PresupuestoActualModel> GetAllPresupuestos()
        {
            return Mapper.Map<List<PrespuestoActual>, List<PresupuestoActualModel>>(oPresupuestoActualRepositorio.GetAllPresupuestos());
        }

        public PresupuestoActualModel GetAllPresupuestos(int idPresupuesto)
        {
            return Mapper.Map<PrespuestoActual, PresupuestoActualModel>(oPresupuestoActualRepositorio.GetAllPresupuestos(idPresupuesto));
        }

        public PresupuestoActualModel GetPresupuestos(int idPresupuesto)
        {
            return Mapper.Map<PrespuestoActual, PresupuestoActualModel>(oPresupuestoActualRepositorio.GetPresupuestos(idPresupuesto));
        }
        public PresupuestoActualModel GetPresupuestosPorCodigo(string codigo)
        {

            return Mapper.Map<PrespuestoActual, PresupuestoActualModel>(oPresupuestoActualRepositorio.GetPresupuestosPorCodigo(codigo));
        }

        public PresupuestoActualModel ActualizarPresupuesto(PresupuestoActualModel oPresupuestoActualModel)
        {
            PrespuestoActual oPresupuestoActual = Mapper.Map<PresupuestoActualModel, PrespuestoActual>(oPresupuestoActualModel);
            return Mapper.Map<PrespuestoActual, PresupuestoActualModel>(oPresupuestoActualRepositorio.ActualizarPresupuesto(oPresupuestoActual));
        }

        public PresupuestoActualModel UpdatePorIngreoCuentaBancaria(BancoCuentaBancariaModel model, CajaGrupoModel cajaGrupoModel)
        {
            PresupuestoActualModel presupuestoActualModel = GetPresupuestosPorCodigo(cajaGrupoModel.Codigo);
            if (presupuestoActualModel != null)
            {
            presupuestoActualModel.Ejecutado += model.Importe;
            presupuestoActualModel.IdUsuario = model.IdUsuario;
            presupuestoActualModel.UltimaModificacion = DateTime.Now;
            ActualizarPresupuesto(presupuestoActualModel);

                ServicioPresupuestoCosto servicioPresupuestoCosto = new ServicioPresupuestoCosto();
                PresupuestoCostoModel oPresupuestoCosto = servicioPresupuestoCosto.GetAllPresupuestoCosto(presupuestoActualModel.Codigo);
                if (oPresupuestoCosto != null)
                {
                    oPresupuestoCosto.Ejecutado += model.Importe;
                    oPresupuestoCosto.IdUsuario = model.IdUsuario;
                    oPresupuestoCosto.UltimaModificacion = DateTime.Now;
                    servicioPresupuestoCosto.ActualizarPresupuesto(oPresupuestoCosto);
                }

                ServicioPresupuestoItem servicioPresupuestoItem = new ServicioPresupuestoItem();

                PresupuestoItemModel presupuestoItem = new PresupuestoItemModel();
                presupuestoItem.Codigo = cajaGrupoModel.Codigo; 
                presupuestoItem.Concepto = model.CuentaDescripcion;
                presupuestoItem.Pagado = model.Importe;
                presupuestoItem.Ejecutado = "true";              
                presupuestoItem.Periodo = int.Parse(DateTime.Now.ToString("yyMM"));

                servicioPresupuestoItem.Insertar(presupuestoItem);
            }
        
            return presupuestoActualModel;
        }
    }
}
