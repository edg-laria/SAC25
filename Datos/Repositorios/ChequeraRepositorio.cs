using System;
using System.Data.Entity;
using Datos.ModeloDeDatos;
using System.Collections.Generic;
using System.Linq;


namespace Datos.Repositorios
{
    public class ChequeraRepositorio : RepositorioBase<Chequera>
    {
        private SAC_Entities context;

        public ChequeraRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<Chequera> GetAllChequera()
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<Chequera> listaChequera = new List<Chequera>();
            listaChequera = context.Chequera
                .Include(b => b.BancoCuenta)
                .Where(p => p.IdProveedor == null
                                                    && p.NumeroRecibo == null
                                                    && p.Activo == true
                                                    && p.Usado == false).ToList();
            listaChequera = listaChequera.OrderBy(p => p.NumeroCheque).ToList();
            return listaChequera;
        }

        public Chequera obtenerCheque(int idCheque)
        {
            return context.Chequera.Where(p => p.Id == idCheque && p.Activo == true).First();
        }

        public Chequera VerificarCheque(int nroCheque)
        {
            return context.Chequera.Where(p => p.NumeroCheque == nroCheque && p.Activo == true).FirstOrDefault();
        }

        public Chequera Actualizar(Chequera oChequera)
        {
            Chequera nChequera = obtenerCheque(oChequera.Id);
            nChequera.Id = oChequera.Id;
            nChequera.NumeroCheque = oChequera.NumeroCheque;
            nChequera.IdBancoCuenta = oChequera.IdBancoCuenta;
            nChequera.Fecha = oChequera.Fecha;
            nChequera.Importes = oChequera.Importes;
            nChequera.IdProveedor = oChequera.IdProveedor;
            nChequera.NumeroRecibo = oChequera.NumeroRecibo;
            nChequera.FechaIngreso = oChequera.FechaIngreso;
            nChequera.FechaEgreso = oChequera.FechaEgreso;
            nChequera.Destino = oChequera.Destino;
            nChequera.IdMoneda = oChequera.IdMoneda;
            nChequera.NumeroOperacion = oChequera.NumeroOperacion;
            nChequera.Registro = oChequera.Registro;
            nChequera.Usado = oChequera.Usado;
            nChequera.Activo = oChequera.Activo;
            nChequera.IdUsuario = oChequera.IdUsuario;
            nChequera.UltimaModificacion = oChequera.UltimaModificacion;
            context.SaveChanges();
            return nChequera;
        }

        public List<Chequera> GetChequePropioPorUsuario(int idUsuario)
        {
            return context.Chequera.Where(p => p.IdUsuario == idUsuario && p.Usado == false).ToList();
        }

        public void DeleteChequePropio(int id)
        {
            Chequera chequera = GetChequePropioPorId(id);
            chequera.Activo = false;
            chequera.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
        }

        public Chequera GetChequePropioPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false; //dev-a
            return context.Chequera
                .Include(b => b.TipoMoneda)
                .Include(b => b.BancoCuenta)
                .Where(p => p.Id == id && p.Activo == true)
                .FirstOrDefault();
        }

        public int GetNroChequePorCta(int id)
        {
            BancoCuenta bancoCuenta = context.BancoCuenta
                                       .Include(b => b.Banco)
                                       .Where(p => p.Id == id && p.Activo == true).FirstOrDefault();
            return bancoCuenta.Banco.NumeroCheque;
        }

        public void ActualizarNumeroCheque(Chequera model)
        {
            BancoCuenta bancoCuenta = context.BancoCuenta.Where(p => p.Id == model.IdBancoCuenta).FirstOrDefault();
            Banco banco = context.Banco.Where(p => p.Id == bancoCuenta.IdBanco).First();
            banco.NumeroCheque = model.NumeroCheque;
            banco.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();
        }


    }

}
