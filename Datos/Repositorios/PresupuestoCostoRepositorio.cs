using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class PresupuestoCostoRepositorio : RepositorioBase<PresupuestoCosto>
    {
        private SAC_Entities context;

        public PresupuestoCostoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<PresupuestoCosto> GetAllPresupuestoCosto()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.PresupuestoCosto.Where(p => p.Activo == true).ToList();

        }

        public PresupuestoCosto GetAllPresupuestoCosto(int Id)
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.PresupuestoCosto.Where(p => p.Activo == true && p.Id == Id).First();

        }

        public PresupuestoCosto GetAllPresupuestoCosto(string codigo)
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.PresupuestoCosto.Where(p => p.Activo == true && p.Codigo== codigo).FirstOrDefault();

        }

        public PresupuestoCosto ActualizarPresupuesto(PresupuestoCosto oPresupuestoCostoIn)
        {

            PresupuestoCosto oPresupueCosto = GetAllPresupuestoCosto(oPresupuestoCostoIn.Id);
            oPresupueCosto.Id = oPresupuestoCostoIn.Id;
            oPresupueCosto.IdImputacion = oPresupuestoCostoIn.IdImputacion;
            oPresupueCosto.IdUsuario = oPresupuestoCostoIn.IdUsuario;
            oPresupueCosto.UltimaModificacion = oPresupuestoCostoIn.UltimaModificacion;
            oPresupueCosto.Historico = oPresupuestoCostoIn.Historico;
            oPresupueCosto.Ejecutado = oPresupuestoCostoIn.Ejecutado;
            oPresupueCosto.Concepto = oPresupuestoCostoIn.Concepto;
            oPresupueCosto.Codigo = oPresupuestoCostoIn.Codigo;
            oPresupueCosto.Actual = oPresupuestoCostoIn.Actual;
            oPresupueCosto.Activo = oPresupuestoCostoIn.Activo;

            context.SaveChanges();
            return oPresupueCosto;

        }



    }
}
