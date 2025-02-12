using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class CajaSaldoRepositorio : RepositorioBase<CajaSaldo>
    {
       private SAC_Entities context;
    
        public CajaSaldoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public CajaSaldo CrearCajaSaldo(CajaSaldo model)
        {
           return  Insertar(model);
        }

        public List<CajaSaldo> GetAllCajaSaldo()
        {
            return context.CajaSaldo.Where(acc => acc.Activo == true).OrderBy(acc => acc.Fecha).ToList();
        }

        public CajaSaldo GetCajaSaldoPorId(int id)
        {           
            return context.CajaSaldo.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }


       


        public CajaSaldo ActualizarCajaSaldo(CajaSaldo Model)
        {

            CajaSaldo GrupoCajaExistente = GetCajaSaldoPorId(Model.Id);

            GrupoCajaExistente.Id = Model.Id;
            GrupoCajaExistente.NumeroCierrre = Model.NumeroCierrre;

            GrupoCajaExistente.ImporteInicialPesos = Model.ImporteInicialPesos;
            GrupoCajaExistente.ImporteInicialDolares = Model.ImporteInicialDolares;
            GrupoCajaExistente.ImporteInicialCheques = Model.ImporteInicialCheques;
            GrupoCajaExistente.ImporteInicialTarjetas = Model.ImporteInicialTarjetas;
            GrupoCajaExistente.ImporteInicialDepositos = Model.ImporteInicialDepositos;


            GrupoCajaExistente.ImporteFinalPesos= Model.ImporteFinalPesos;
            GrupoCajaExistente.ImporteFinalDolares = Model.ImporteFinalDolares;
            GrupoCajaExistente.ImporteFinalCheques = Model.ImporteFinalCheques;
            GrupoCajaExistente.ImporteFinalTarjetas = Model.ImporteFinalTarjetas;
            GrupoCajaExistente.ImporteFinalDepositos = Model.ImporteFinalDepositos;

            GrupoCajaExistente.Fecha = Model.Fecha;

            GrupoCajaExistente.UltimaModificacion = Model.UltimaModificacion;
            GrupoCajaExistente.IdUsuario= Model.IdUsuario;

            
           


            context.SaveChanges();

            return GrupoCajaExistente;





        }

        public CajaSaldo GetUltimoCierre()
        {

            return context.CajaSaldo
                        .Where(acc=> acc.Activo == true)
                        .OrderByDescending(acc => acc.NumeroCierrre)
                        .FirstOrDefault();          
        }

        public int GetNuevoNumeroCierre()
        {
            return context.CajaSaldo.Where(acc => acc.Activo == true).Max( n => n.NumeroCierrre) + 1;
        }

        public int DeleteCaja(int IdCaja)
        {
            CajaSaldo CajaCaja = GetCajaSaldoPorId(IdCaja);
            CajaCaja.Activo = false;
            CajaCaja.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }

        public CajaSaldo ActualizarImporteCierreCajaSaldo(CajaSaldo Model)
        {
            CajaSaldo GrupoCajaExistente = GetCajaSaldoPorId(Model.Id);
            GrupoCajaExistente.ImporteFinalPesos = Model.ImporteFinalPesos;
            GrupoCajaExistente.ImporteFinalDolares = Model.ImporteFinalDolares;
            GrupoCajaExistente.ImporteFinalCheques = Model.ImporteFinalCheques;
            GrupoCajaExistente.ImporteFinalTarjetas = Model.ImporteFinalTarjetas;
            GrupoCajaExistente.ImporteFinalDepositos = Model.ImporteFinalDepositos;
            GrupoCajaExistente.UltimaModificacion = Model.UltimaModificacion;
            GrupoCajaExistente.IdUsuario = Model.IdUsuario;
            context.SaveChanges();
            return GrupoCajaExistente;
        }
    }
}