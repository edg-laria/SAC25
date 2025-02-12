using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Z.EntityFramework.Plus;

namespace Datos.Repositorios
{
   public class ImputacionRepositorio : RepositorioBase<Imputacion>
    {

        private SAC_Entities context;

        public ImputacionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public Imputacion InsertarImputacion(Imputacion Imputacion)
        {
            Imputacion.Activo = true;
            return Insertar(Imputacion);
        }


        public Imputacion ObtenerImputacion(int idImputacion)
        {
            context.Configuration.LazyLoadingEnabled = false;
            Imputacion Imputacion = context.Imputacion.Where(p => p.Id == idImputacion).First();
            return Imputacion;
        }


        public Imputacion ObtenerImputacionPorId(int idImputacion)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var Imputacion = context.Imputacion.Where(p => p.Id == idImputacion).FirstOrDefault();
            return Imputacion;
        }

        public Imputacion ActualizarImputacion(Imputacion model)
        {
            Imputacion ImputacionExistente = ObtenerImputacionPorId(model.Id);

            ImputacionExistente.Id = model.Id;
            ImputacionExistente.Descripcion = model.Descripcion;
            ImputacionExistente.IdSubRubro = model.IdSubRubro;
            ImputacionExistente.SaldoInicial = model.SaldoInicial;
            ImputacionExistente.SaldoFin = model.SaldoFin;
            ImputacionExistente.IdTipo  = model.IdTipo;
            ImputacionExistente.Alias = model.Alias;
            ImputacionExistente.Enero = model.Enero;
            ImputacionExistente.Febrero = model.Febrero;
            ImputacionExistente.Marzo = model.Marzo;
            ImputacionExistente.Abril = model.Abril;
            ImputacionExistente.Mayo = model.Mayo;
            ImputacionExistente.Junio = model.Junio;
            ImputacionExistente.Julio = model.Julio;
            ImputacionExistente.Agosto= model.Agosto;
            ImputacionExistente.Septiembre = model.Septiembre;
            ImputacionExistente.Octubre = model.Octubre;
            ImputacionExistente.Noviembre = model.Noviembre;
            ImputacionExistente.Diciembre = model.Diciembre;
            ImputacionExistente.Activo = model.Activo;
            ImputacionExistente.IdUsuario = model.IdUsuario;
            ImputacionExistente.UltimaModificacion = model.UltimaModificacion;

            context.SaveChanges();
            return ImputacionExistente;
        }

    
        public Imputacion ObtenerImputacionPorDescripcion(string oDescripcion)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Imputacion.Where(p => p.Descripcion == oDescripcion).FirstOrDefault();
        }

        public Imputacion ObtenerImputacionPorDescripcion(string oDescripcion, int oCodigo)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Imputacion.Where(p => p.Descripcion == oDescripcion && p.Id== oCodigo).FirstOrDefault();
        }


        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idImputacion"></param>
        /// <returns></returns>
        public Imputacion ObtenerImputacionPorDescripcion(string oDescripcion, int oIdSubrubro, int oIdImputacion)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Imputacion.Where(p => p.Descripcion == oDescripcion && p.IdSubRubro == oIdSubrubro && p.Id != oIdImputacion).FirstOrDefault();
        }



        public List<Imputacion> GetAllImputacion()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Imputacion> listaImputacion = context.Imputacion.Where(P=> P.Activo == true).ToList();
            listaImputacion = listaImputacion.OrderBy(p => p.Descripcion).ToList();
            return listaImputacion;
        }

     

        public int EliminarImputacion(int idImputacion)
        {

            Imputacion ImputacionExistente = ObtenerImputacionPorId(idImputacion);
            ImputacionExistente.Activo = false;
            context.SaveChanges();
            return 1;

        }

        public Imputacion GetImputacionPorAlias(string alias)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Imputacion.Where(p => p.Alias == alias).FirstOrDefault();
             
        }

        public void ActualizarAsientoImputacion(Imputacion model)
        {
            Imputacion imputacion = ObtenerImputacionPorId(model.Id);           
            imputacion.SaldoInicial = model.SaldoInicial;
            imputacion.SaldoFin = model.SaldoFin;
            imputacion.Enero = model.Enero;
            imputacion.Febrero = model.Febrero;
            imputacion.Marzo = model.Marzo;
            imputacion.Abril = model.Abril;
            imputacion.Mayo = model.Mayo;
            imputacion.Junio = model.Junio;
            imputacion.Julio = model.Julio;
            imputacion.Agosto = model.Agosto;
            imputacion.Septiembre = model.Septiembre;
            imputacion.Octubre = model.Octubre;
            imputacion.Noviembre = model.Noviembre;
            imputacion.Diciembre = model.Diciembre;
            imputacion.Activo = model.Activo;
            imputacion.UltimaModificacion = model.UltimaModificacion;
            context.SaveChanges();

        }

    //    public List<Diario> GetCompraFactura_(string Periodo)
    //    {
    //        return context.Diario                    
    //                 .Where(d => d.Tipo == "CF" && d.Periodo==Periodo && d.Activo==true)
    //                 .Select(u => new 
    //                {  
    //                     Descripcion =  u.Descripcion,
    //                    Debe =  1500, //(u.Importe > 0) ? u.Importe : 0,
    //                    Haber =1500 //(u.Importe < 0) ? u.Importe : 0
    //                }).ToList();
    //}

        // en desuso ej. para sp
        public List<Diario> GetCompraFactura(string Periodo)
        {
            List<Diario> lista = new List<Diario>();
            try
            {
                SqlParameter param1 = new SqlParameter("@Periodo", Periodo);
                 lista = context.Database.SqlQuery<Diario>("Diario_of_Periodo @Periodo", param1).ToList();
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                lista = null;               
            }
            return lista;

        }

        public List<Diario> GetAsientosContables(string periodo, string tipo)
        {
            List<Diario> lista = context.Diario.Include(x => x.Imputacion).Where(i => i.Activo == true && i.Tipo == tipo  && i.Periodo == periodo).ToList();
           // Diario.Include(x => x.Imputacion).Where(i => i.Activo == true && i.Tipo == "VF" && i.Periodo == "2204").ToList()
            return lista;
        }


        public List<GrupoCuenta> GetPlanContable()
        {
            return context.GrupoCuenta.Where(P => P.Activo == true).ToList();
        }

        public List<GrupoCuenta> GetGrupoCuentaContable()
        {
            return context.GrupoCuenta.Where(g => g.Activo == true ).ToList();
        }

        public List<Rubro> GetRubroContable()
        {
            return context.Rubro.Where(g => g.Activo == true).ToList();
        }

        public List<SubRubro> GetSubRubroContable()
        {
            return context.SubRubro.Where(g => g.Activo == true).ToList();
        }

        public List<Imputacion> GetImputacionContable()
        {
            return context.Imputacion.Where(g => g.Activo == true ).ToList();
        }



        public GrupoCuenta GetGrupoCuentaContable(int id)
        {
            return context.GrupoCuenta.Where(g => g.Activo == true & g.Id == id ).FirstOrDefault();
        }

        public Rubro GetRubroContable(int id)
        {
            return context.Rubro.Where(g => g.Activo == true & g.Id == id).FirstOrDefault();
        }

        public SubRubro GetSubRubroContable(int id)
        {
            return context.SubRubro.Where(g => g.Activo == true & g.Id == id).FirstOrDefault();
        }

        public Imputacion GetImputacionContable(int id)
        {
            return context.Imputacion.Where(g => g.Activo == true & g.Id == id).FirstOrDefault();
        }

        public GrupoCuenta updateGrupoCuentaContable(GrupoCuenta model)
        {
            var entidad = GetGrupoCuentaContable(model.Id);
            if (entidad != null){
            entidad.Id = model.Id;
            entidad.Descripcion = model.Descripcion;
            entidad.Activo = model.Activo;
            entidad.UltimaModificacion = model.UltimaModificacion;
            context.SaveChanges(); 
            }
            return entidad;
        }

        public Rubro updateRubroContable(Rubro model)
        {
            var entidad = GetRubroContable(model.Id);
            if (entidad != null)
            {
                entidad.Id = model.Id;
                entidad.Descripcion = model.Descripcion;
                entidad.IdGrupoCuenta = model.IdGrupoCuenta;
                entidad.Activo = model.Activo;
                entidad.UltimaModificacion = model.UltimaModificacion;
                context.SaveChanges();
            }
            return entidad;
        }

        public SubRubro updateSubRubroContable(SubRubro model)
        {
            var entidad = GetSubRubroContable(model.Id);
            if (entidad != null)
            {
                entidad.Id = model.Id;
                entidad.Descripcion = model.Descripcion;
                entidad.IdRubro = model.IdRubro;
                entidad.Activo = model.Activo;
                entidad.UltimaModificacion = model.UltimaModificacion;
                context.SaveChanges();
            }
            return entidad;
        }

        public Imputacion updateImputacionContable(Imputacion model)
        {
            var entidad = GetImputacionContable(model.Id);
            if (entidad != null)
            {
                entidad.Id = model.Id;
                entidad.Descripcion = model.Descripcion;
                entidad.IdSubRubro = model.IdSubRubro;
                entidad.Activo = model.Activo;
                entidad.UltimaModificacion = model.UltimaModificacion;
                context.SaveChanges();
            }
            return entidad;
        }

        public GrupoCuenta InsertGrupoCuentaContable(GrupoCuenta model)
        {
            var entidad = GetGrupoCuentaContable(model.Id);
            if (entidad == null)
            {
                context.GrupoCuenta.Add(model);               
                context.SaveChanges();
            }
            return model;
        }

        public Rubro InsertRubroContable(Rubro model)
        {
            var entidad = GetRubroContable(model.Id);
            if (entidad == null)
            {
                context.Rubro.Add(model);
                context.SaveChanges();
            }
            return model;
        }

        public SubRubro InsertSubRubroContable(SubRubro model)
        {
            var entidad = GetSubRubroContable(model.Id);
            if (entidad == null)
            {
                context.SubRubro.Add(model);
                context.SaveChanges();
            }
            return model;
        }

        public Imputacion InsertImputacionContable(Imputacion model)
        {
            var entidad = GetImputacionContable(model.Id);
            if (entidad == null)
            {
                InsertarImputacion(model);
            }
            return model;
          
        }

        public List<Diario> GetAsientoContableDetalles(int cuenta, string periodo, string tipo)
        {
        
            List<Diario> lista = context.Diario.Where(i => i.Activo == true
            && i.Tipo == tipo.Replace(" ", String.Empty)
            && i.Periodo == periodo.Replace(" ", String.Empty)
            && i.IdImputacion == cuenta).ToList();
            return lista;
        }
        public List<Diario> GetAsientoContableDetalles(string periodo, string tipo)
        {
            List<Diario> lista = context.Diario.Where(i => i.Activo == true
         && i.Tipo == tipo.Replace(" ", String.Empty)
         && i.Periodo == periodo.Replace(" ", String.Empty)
        ).ToList();
            return lista;
        }

    }
}
