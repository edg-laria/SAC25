using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class CajaRepositorio : RepositorioBase<Caja>
    {
       private SAC_Entities context;
    
        public CajaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public Caja CrearCaja(Caja model)
        {
           return  Insertar(model);
        }

        public List<Caja> GetAllCaja()
        {
            return context.Caja.Where(acc => acc.Activo == true && (acc.IdCajaSaldo==0 || acc.IdCajaSaldo == null)  ).OrderBy(acc => acc.Id).ToList();
        }

        public Caja GetCajaPorId(int id)
        {           
            return context.Caja.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }


        public Caja GetCajaPorGrupo(int idgrupocaja)
        {
            return context.Caja.Where(acc => acc.IdGrupoCaja == idgrupocaja && acc.Activo == true).FirstOrDefault();
        }


        public Caja ActualizarCaja(Caja Model)
        {

            Caja GrupoCajaExistente = GetCajaPorId(Model.Id);

            GrupoCajaExistente.Id = Model.Id;
            GrupoCajaExistente.Concepto = Model.Concepto;
            GrupoCajaExistente.IdGrupoCaja = Model.IdGrupoCaja;
            GrupoCajaExistente.UltimaModificacion = Model.UltimaModificacion;
            GrupoCajaExistente.IdUsuario= Model.IdUsuario;
            GrupoCajaExistente.ImporteCheque= Model.ImporteCheque;
            GrupoCajaExistente.ImporteDeposito = Model.ImporteDeposito;
            GrupoCajaExistente.ImporteDolar = Model.ImporteDolar;
            GrupoCajaExistente.ImporteTarjeta = Model.ImporteTarjeta;
            GrupoCajaExistente.ImportePesos = Model.ImportePesos;

            context.SaveChanges();
            return GrupoCajaExistente;
        }

        public List<Caja> GetAllCajaPorIdCierre(int v)
        {
            return context.Caja
                    .Where(x => x.Activo == true && x.IdCajaSaldo == v || x.IdCajaSaldo == null)
                    .OrderByDescending(acc => acc.Fecha)
                    .ToList();
        }

        public int DeleteCaja(int IdCaja)
        {
            Caja CajaCaja = GetCajaPorId(IdCaja);
            CajaCaja.Activo = false;
            CajaCaja.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }

        public List<Caja> getGrupoCajaFecha(int idgrupocaja, DateTime fechadesde, DateTime fechahasta)
         {

            List<Caja> p = (from c in context.Caja
                            where c.Activo == true 
                            && c.IdGrupoCaja == idgrupocaja 
                            && c.Fecha >= fechadesde 
                            && c.Fecha <= fechahasta
                            select c).ToList();
            // return context.Caja.Where(acc => acc.Activo == true && acc.IdGrupoCaja && acc.Fecha >=fechadesde && acc.Fecha <= fechahasta).OrderBy(acc => acc.Id).ToList();
            return p;

        }

        public List<Caja> GetSaldoInicialCaja(int idgrupocaja, DateTime fechadesde)
        {
            List<Caja> p = (from c in context.Caja
                            where c.Activo == true && c.IdGrupoCaja == idgrupocaja && c.Fecha <= fechadesde
                            select c).ToList();
            return p;

            /*
            return context.Caja.Where(acc => acc.Activo == true && acc => AccessViolationException. == idgrupocaja && c.Fecha <= fechadesde).OrderByDescending(acc => acc.NumeroCierrre).FirstOrDefault();

            */
        }

        public Caja GetCajaPorFecha(int idgrupocaja , DateTime fecha)
        {        

            //1) se obtiene un objeto anonimo y no es posible pasarlo a una entidad
            var iQuery = (  from c in context.Caja
                            where c.IdGrupoCaja == idgrupocaja && c.Fecha < fecha
                            group c by c.IdGrupoCaja into g
                            select new 
                            {
                                IdGrupoCaja = g.Key,
                                ImportePesos = g.Sum(c => c.ImportePesos),
                                ImporteCheque = g.Sum(c => c.ImporteCheque),
                                ImporteDolar = g.Sum(c => c.ImporteDolar),
                                ImporteDeposito = g.Sum(c => c.ImporteDeposito),
                                ImporteTarjeta = g.Sum(c => c.ImporteTarjeta)
                            });
            //2) convertimos el objeto anonimo en una entidad
            var caja = iQuery.ToList().Select(i => new Caja
            {
                IdGrupoCaja = i.IdGrupoCaja,
                ImportePesos = i.ImportePesos,
                ImporteCheque = i.ImporteCheque,
                ImporteDolar = i.ImporteDolar,
                ImporteDeposito = i.ImporteDeposito,
                ImporteTarjeta = i.ImporteTarjeta
            }).FirstOrDefault();

          
            return caja;
        }

        public Caja GetCajaMenorIgualAFecha(int idgrupocaja, DateTime fecha)
        {

            //1) se obtiene un objeto anonimo y no es posible pasarlo a una entidad
            var iQuery = (from c in context.Caja
                          where c.IdGrupoCaja == idgrupocaja && c.Fecha <= fecha
                          group c by c.IdGrupoCaja into g
                          select new
                          {
                              IdGrupoCaja = g.Key,
                              ImportePesos = g.Sum(c => c.ImportePesos),
                              ImporteCheque = g.Sum(c => c.ImporteCheque),
                              ImporteDolar = g.Sum(c => c.ImporteDolar),
                              ImporteDeposito = g.Sum(c => c.ImporteDeposito),
                              ImporteTarjeta = g.Sum(c => c.ImporteTarjeta)
                          });
            //2) convertimos el objeto anonimo en una entidad
            var caja = iQuery.ToList().Select(i => new Caja
            {
                IdGrupoCaja = i.IdGrupoCaja,
                ImportePesos = i.ImportePesos,
                ImporteCheque = i.ImporteCheque,
                ImporteDolar = i.ImporteDolar,
                ImporteDeposito = i.ImporteDeposito,
                ImporteTarjeta = i.ImporteTarjeta
            }).FirstOrDefault();


            return caja;
        }

        public Caja GetCajaPorFechaHasta(int idgrupocaja, DateTime fecha)
        {

            //Ejemplo 
            //        var query = from p in db.Products
            //                    where p.CategoryID == categoryID
            //                    select new { Name = p.Name };
            //        var products = query.ToList().Select(r => new Product
            //        {
            //            Name = r.Name;
            //        }).ToList();

            //        return products;

            //1) se obtiene un objeto anonimo y no es posible pasarlo a una entidad
            var iQuery = (from c in context.Caja
                          where c.IdGrupoCaja == idgrupocaja && c.Fecha <= fecha
                          group c by c.IdGrupoCaja into g
                          select new
                          {
                              IdGrupoCaja = g.Key,
                              ImportePesos = g.Sum(c => c.ImportePesos),
                              ImporteCheque = g.Sum(c => c.ImporteCheque),
                              ImporteDolar = g.Sum(c => c.ImporteDolar),
                              ImporteDeposito = g.Sum(c => c.ImporteDeposito),
                              ImporteTarjeta = g.Sum(c => c.ImporteTarjeta)
                          });
            //2) convertimos el objeto anonimo en una entidad
            var caja = iQuery.ToList().Select(i => new Caja
            {
                IdGrupoCaja = i.IdGrupoCaja,
                ImportePesos = i.ImportePesos,
                ImporteCheque = i.ImporteCheque,
                ImporteDolar = i.ImporteDolar,
                ImporteDeposito = i.ImporteDeposito,
                ImporteTarjeta = i.ImporteTarjeta
            }).FirstOrDefault();


            return caja;
        }

        public object ActualizarCierreCaja(Caja model)
        {
            Caja GrupoCajaExistente = GetCajaPorId(model.Id);           
            GrupoCajaExistente.IdCajaSaldo = model.IdCajaSaldo;

            GrupoCajaExistente.UltimaModificacion = model.UltimaModificacion;
            context.SaveChanges();
            return GrupoCajaExistente;
        }
    }
}