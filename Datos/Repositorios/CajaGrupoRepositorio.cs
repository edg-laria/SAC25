using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class CajaGrupoRepositorio : RepositorioBase<GrupoCaja>
    {
       private SAC_Entities context;
    
        public CajaGrupoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public GrupoCaja CreateAccion(GrupoCaja model)
        {
           return  Insertar(model);
        }

        public List<GrupoCaja> GetAllGrupoCaja()
        {
            return context.GrupoCaja.Where(acc => acc.Activo == true).OrderBy(acc => acc.Codigo).ToList();
        }

        public GrupoCaja GetGrupoCajaPorId(int id)
        {           
            return context.GrupoCaja.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }


        public GrupoCaja GetGrupoCajaPorCodigo(string codigo)
        {
            return context.GrupoCaja.Where(acc => acc.Codigo == codigo && acc.Activo == true).FirstOrDefault();
        }



        public GrupoCaja ActualizarGrupoCaja(GrupoCaja Model)
        {

            GrupoCaja GrupoCajaExistente = GetGrupoCajaPorId(Model.Id);

            GrupoCajaExistente.Id = Model.Id;
            GrupoCajaExistente.Codigo = Model.Codigo;
            GrupoCajaExistente.Concepto = Model.Concepto;
            GrupoCajaExistente.IdImputacion = Model.IdImputacion;
            context.SaveChanges();

            return GrupoCajaExistente;





        }



        public int DeleteGrupoCaja(int IdGrupoCaja)
        {
            GrupoCaja GrupoCaja = GetGrupoCajaPorId(IdGrupoCaja);
            GrupoCaja.Activo = false;
          
            GrupoCaja.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
            return 1;

        }


        private List<GrupoCaja> ListadoGrupoCaja()
        {
            return context.GrupoCaja.OrderBy(acc => acc.Codigo).ToList();
        }

        public int CerrarGrupoCaja()


        {

            //GrupoCaja GrupoCajaExistente = new GrupoCaja();

            List<GrupoCaja> GrupoCajaExistente = GetAllGrupoCaja();

            GrupoCajaExistente.ForEach(a =>
            {
                a.ParcialPesos = 0;
                a.ParcialDolares = 0;
                a.ParcialTarjetas = 0;
                a.ParcialCheques= 0;
                a.ParcialDepositos = 0;
            });

                     

           
            context.SaveChanges();

            return 1;



        }
    }
}