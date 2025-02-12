using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class FacturaElectronicaRepositorio : RepositorioBase<FacturaElectronica>
    {
        private SAC_Entities context;

        public FacturaElectronicaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<FacturaElectronica> GetAllFacturasElectonicas()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.FacturaElectronica.ToList();

        }

        public FacturaElectronica GetFacturasElectonicasPorId(int tipoComprobante, int idPuntoVenta, int nroCbte)
        {
            return context.FacturaElectronica.Where(p => p.ID_TIPOCBTE == tipoComprobante && p.PUNTOVTA == idPuntoVenta && p.NROCBTE_AFIP == nroCbte).First();
        }

        public FacturaElectronica Agregar(FacturaElectronica oFacturaElectronica)
        {
            return Insertar(oFacturaElectronica);
        }


        public FacturaElectronica Actualizar(FacturaElectronica model)
        {
            FacturaElectronica factura = GetFacturasElectonicasPorId((int)model.ID_TIPOCBTE, (int)model.PUNTOVTA, (int)model.NROCBTE_AFIP);
            factura.QR = model.QR;
            factura.OBS = model.OBS;
            factura.XMLRES = model.XMLRES;
            factura.CAE = model.CAE;
            factura.FECHAVTO = model.FECHAVTO;
            factura.ID_CBTE_WSAFIP = model.ID_CBTE_WSAFIP;
            factura.NROAUX = model.NROAUX;
            factura.NROCBTE_AFIP = Int32.Parse(model.NROAUX);
            context.SaveChanges();
            return factura;
        }
    }
                /// // 
    }
