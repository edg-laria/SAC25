using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class PresupuestoActualRepositorio : RepositorioBase<PrespuestoActual>
    {

        private SAC_Entities context;

        public PresupuestoActualRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<PrespuestoActual> GetAllPresupuestos()
        {
            return context.PrespuestoActual.Where(p => p.Activo == true).ToList();
        }
        public List<PrespuestoActual> GetPresupuestoCliente()
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.PrespuestoActual.Where(p => p.Activo == true && p.CodigoCaja.Contains("INGLOC") || p.CodigoCaja.Contains("INGEXT")).OrderBy(acc => acc.Id).ToList();
        }
        public PrespuestoActual GetAllPresupuestos(int idPresupuesto)
        {
            return context.PrespuestoActual.Where(p => p.Activo == true && p.Id == idPresupuesto).First();
        }
        public PrespuestoActual GetPresupuestos(int idPresupuesto)
        {
            return context.PrespuestoActual.Where(p => p.Activo == true && p.Id == idPresupuesto).First();
        }

        public PrespuestoActual ActualizarPresupuesto(PrespuestoActual oPresupuestoActual)
        {

            PrespuestoActual oPresupueActual = GetAllPresupuestos(oPresupuestoActual.Id);
            oPresupueActual.Id = oPresupuestoActual.Id;
            oPresupueActual.IdImputacion = oPresupuestoActual.IdImputacion;
            oPresupueActual.IdUsuario = oPresupuestoActual.IdUsuario;
            oPresupueActual.Periodo = oPresupuestoActual.Periodo;
            oPresupueActual.Proveedor = oPresupuestoActual.Proveedor;
            oPresupueActual.UltimaModificacion = oPresupuestoActual.UltimaModificacion;
            oPresupueActual.Historico = oPresupuestoActual.Historico;
            oPresupueActual.Ejecutado = oPresupuestoActual.Ejecutado;
            oPresupueActual.Costos = oPresupuestoActual.Costos;
            oPresupueActual.Concepto = oPresupuestoActual.Concepto;
            oPresupueActual.CodigoCaja = oPresupuestoActual.CodigoCaja;
            oPresupueActual.Codigo = oPresupuestoActual.Codigo;
            oPresupueActual.Actual = oPresupuestoActual.Actual;
            oPresupueActual.Activo = oPresupuestoActual.Activo;
            context.SaveChanges();
            return oPresupueActual;

        }

        public PrespuestoActual GetPresupuestosPorCodigo(string codigo)
        {
            return context.PrespuestoActual.Where(p => p.Activo == true && p.Codigo == codigo).FirstOrDefault();
        }
    }
}
