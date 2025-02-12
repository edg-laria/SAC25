using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using Negocio.Modelos;
using AutoMapper;
using System.Net.Mail;
using System.IO;
using System.Net;
using Negocio.Servicios;
using System.Net.Mime;
using System.Text;
using System.Linq;
using Negocio.Enumeradores;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Negocio.Helpers;

namespace Negocio.Servicios
{
   public class ServicioImputacion : ServicioBase
    {

        private ImputacionRepositorio ImputacionRepositorio;

        public ServicioImputacion()
        {
            ImputacionRepositorio = kernel.Get<ImputacionRepositorio>();
        }


        public List<ImputacionModel> GetAllImputacions()
        {
            try
            {
                var Imputacions = Mapper.Map<List<Imputacion>, List<ImputacionModel>>(ImputacionRepositorio.GetAllImputacion());
                return Imputacions;
            }
            catch (Exception ex)
            {
                 _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }

       

        public ImputacionModel GetImputacion(int _id)
        {
            //modifico esto porque da error de bucle inf bre 06/02/2021
            //Imputacion oImputacion = 
            //ImputacionModel oImputacionModel = new ImputacionModel();
            return Mapper.Map<Imputacion, ImputacionModel>(ImputacionRepositorio.ObtenerImputacionPorId(_id));

        }

        public int ActualizarImputacion(ImputacionModel oImputacionModel)
        {
            //controlar que no exista 
            Imputacion oImputacion = ImputacionRepositorio.ObtenerImputacionPorDescripcion(oImputacionModel.Descripcion, oImputacionModel.IdSubRubro ?? 0, oImputacionModel.Id);
            if (oImputacion != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {


                var imp =  Mapper.Map<ImputacionModel, Imputacion> (oImputacionModel);

                Imputacion oPaisRespuesta = ImputacionRepositorio.ActualizarImputacion(imp);

                if (oPaisRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public List<DiarioModel> GetAsientoContableDetalles(string periodo, string tipo)
        {
            try
            {
                List<Diario> diario = ImputacionRepositorio.GetAsientoContableDetalles(periodo, tipo);
                List<DiarioModel> asientos = new List<DiarioModel>();

                switch (tipo)
                {

                    case "VF":
                        asientos = diario.Select(c => new DiarioModel()
                                            {
                                                IdImputacion = c.IdImputacion,
                                                Descripcion = c.DescripcionMa,
                                                Tipo = tipo,
                                                Periodo = periodo,
                                                Debe = (c.IdImputacion == 110502000 || c.IdImputacion == 110501000) ? c.Importe : 0,
                                                Haber = (c.IdImputacion != 110502000 && c.IdImputacion != 110501000) ? c.Importe : 0
                                            }).ToList();
                        break;
                    case "VP":
                        asientos = diario.Select(c => new DiarioModel()
                                                     {
                                                         IdImputacion = c.IdImputacion,
                                                         Descripcion = c.DescripcionMa,
                                                         Tipo = tipo,
                                                         Periodo = periodo,
                                                         Debe = (c.IdImputacion != 110502000 && c.IdImputacion != 110501000) ? c.Importe : 0,
                                                         Haber = (c.IdImputacion == 110502000 || c.IdImputacion == 110501000) ? c.Importe : 0
                                                     }).ToList();
                        break;
                    case "CP":
                        asientos = diario.Select(c => new DiarioModel()
                               {
                                   IdImputacion = c.IdImputacion,
                                   Descripcion = c.DescripcionMa,
                                   Tipo = tipo,
                                   Periodo = periodo,
                                   Debe =  (c.IdImputacion == 210102000) ? c.Importe : 0,
                                   Haber = (c.IdImputacion != 210102000) ? c.Importe : 0
                               }).ToList();
                        break;
                    default:
                        tipo = (!string.IsNullOrEmpty(tipo)) ? tipo : "CF";
                        asientos = diario.Select(c => new DiarioModel()
                               {
                                   IdImputacion = c.IdImputacion,
                                   Descripcion = c.Descripcion,
                                   Tipo = tipo,
                                   Periodo = periodo,
                                   Debe = (c.IdImputacion != 210102000) ? c.Importe : 0,
                                   Haber = (c.IdImputacion == 210102000) ? c.Importe : 0
                               }).ToList();
                        break;
                }


                return asientos.Where(n => n.Debe != 0 || n.Haber != 0).OrderByDescending(n => n.Debe).ToList();
                //return Mapper.Map<List<Diario>, List<DiarioModel>>(diario);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public List<MenuSideBarModel> GetPlanContable()
        {
            IList<GrupoCuenta> grupoCuentas = ImputacionRepositorio.GetPlanContable();
            ICollection<MenuSideBarModel> node = new List<MenuSideBarModel>();


            var grupos = grupoCuentas
                               .Select(c => new MenuSideBarModel()
                               {
                                   IdMenuSidebar = c.Id,
                                   Titulo = c.Descripcion,
                                   Icono = "fa-book",
                                   Url = "0-",
                                   IdParent = 0
                                   ,Group = c.Rubro.Where( r => r.Activo == true)
                                        .Select(r => new MenuSideBarModel()
                                                           {
                                                               IdMenuSidebar = r.Id,
                                                               Titulo = r.Descripcion,
                                                               Icono = "fa-book",
                                                               Url = "1-",
                                                               IdParent = r.IdGrupoCuenta,
                                                               Group = r.SubRubro.Where(s => s.Activo == true)
                                                               .Select(s => new MenuSideBarModel()
                                                               {
                                                                   IdMenuSidebar = s.Id,
                                                                   Titulo = s.Descripcion,
                                                                   Icono = "fa-book",
                                                                   Url = "2-",
                                                                   IdParent = s.IdRubro,
                                                                   Group = s.Imputacion.Where(i => i.Activo == true)
                                                                   .Select(i => new MenuSideBarModel()
                                                                   {
                                                                       IdMenuSidebar = i.Id,
                                                                       Titulo = i.Descripcion,
                                                                       Icono = "fa-book",
                                                                       Url = "3-",
                                                                       IdParent = i.IdSubRubro,
                                                                       Group = new List<MenuSideBarModel>()
                                                                   }).ToList()
                                                               }).ToList()
                                                           }).ToList()
                                   
                               }).ToList();

            return grupos;
        }

     
        public void AsintoContableGeneral(Diario model)
        {
            try
            {
                /// BUSCAR LA CTA            
                ImputacionModel cta = GetImputacion(model.IdImputacion);
                /// UPDATE
                /// SALDO FIN 
                /// SALDO MES 
                switch (model.Fecha.Month)
                {
                    case 1:
                        cta.Enero = cta.Enero ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 2:
                        cta.Febrero = cta.Febrero ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 3:
                        cta.Marzo = cta.Marzo ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 4:
                        cta.Abril = cta.Abril ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 5:
                        cta.Mayo = cta.Mayo ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 6:
                        cta.Junio = cta.Junio ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 7:
                        cta.Julio = cta.Julio ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 8:
                        cta.Agosto = cta.Agosto ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 9:
                        cta.Septiembre = cta.Septiembre ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 10:
                        cta.Octubre = cta.Octubre ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 11:
                        cta.Noviembre = cta.Noviembre ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 12:
                        cta.Diciembre = cta.Diciembre ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    default:
                        throw new InvalidOperationException("unknown item type");
                }

                cta.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                cta.Activo = true;
                ImputacionRepositorio.ActualizarAsientoImputacion(Mapper.Map<ImputacionModel, Imputacion>(cta));
                 _mensaje?.Invoke("Se registro el asineto contable correctamente", "ok");



            }
            catch (Exception ex)
            {
                NLogHelper.Instance.LogExcepcion(ex, "ServicioImputacion >> AsintoContableGeneral");
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                throw new Exception("No pudo registar el Asiento Contable");
            }
        }


        public List<Imputacion> GetImputacionContable()
        {
            List<Imputacion> imputacions = ImputacionRepositorio.GetImputacionContable();
            return imputacions;
        }

        public List<SubRubro> GetSubRubroContable()
        {
            List<SubRubro> subRubros = ImputacionRepositorio.GetSubRubroContable();
            return subRubros;
        }

        public List<Rubro> GetRubroContable()
        {
            List<Rubro> rubros = ImputacionRepositorio.GetRubroContable();
            return rubros;
        }

        public List<GrupoCuenta> GetGrupoCuentaContable()
        {
            List<GrupoCuenta> grupoCuentas = ImputacionRepositorio.GetGrupoCuentaContable();
            return grupoCuentas;
        }

        public Imputacion GetImputacionContable( int i)
        {
            Imputacion imputacions = ImputacionRepositorio.GetImputacionContable(i);
            return imputacions;
        }

        public SubRubro GetSubRubroContable(int s)
        {
           SubRubro subRubros = ImputacionRepositorio.GetSubRubroContable(s);
            return subRubros;
        }

        public Rubro GetRubroContable( int r)
        {
            Rubro rubros = ImputacionRepositorio.GetRubroContable(r);
            return rubros;
        }

        public GrupoCuenta GetGrupoCuentaContable(int g)
        {
            GrupoCuenta grupoCuentas = ImputacionRepositorio.GetGrupoCuentaContable(g);
            return grupoCuentas;
        }




        public ImputacionModel GetImputacionPorAlias(string alias)
        {
            try
            {
                return Mapper.Map<Imputacion, ImputacionModel>(ImputacionRepositorio.GetImputacionPorAlias(alias));
            }
            catch (Exception)
            {
                  _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public int GuardarImputacion(ImputacionModel oImputacionModel)
        {
            //controlar que no exista 
            Imputacion oImputacion = ImputacionRepositorio.ObtenerImputacionPorDescripcion(oImputacionModel.Descripcion, oImputacionModel.Id);
            if (oImputacion != null)
            {
                return -2;
            }
            else
            {
                Imputacion oImputacionNuevo = new Imputacion();
                Imputacion oImputacionRespuesta = new Imputacion();

                oImputacionNuevo.Id = oImputacionModel.Id;
                oImputacionNuevo.Descripcion = oImputacionModel.Descripcion;
                oImputacionNuevo.IdSubRubro = oImputacionModel.IdSubRubro;
                oImputacionNuevo.SaldoInicial = oImputacionModel.SaldoInicial;
                oImputacionNuevo.SaldoFin = oImputacionModel.SaldoFin;
                oImputacionNuevo.IdTipo = oImputacionModel.IdTipo;
                oImputacionNuevo.Alias = oImputacionModel.Alias;
                oImputacionNuevo.Enero = oImputacionModel.Enero;
                oImputacionNuevo.Febrero = oImputacionModel.Febrero;
                oImputacionNuevo.Marzo = oImputacionModel.Marzo;
                oImputacionNuevo.Abril = oImputacionModel.Abril;
                oImputacionNuevo.Mayo = oImputacionModel.Mayo;
                oImputacionNuevo.Junio = oImputacionModel.Junio;
                oImputacionNuevo.Julio = oImputacionModel.Julio;
                oImputacionNuevo.Agosto = oImputacionModel.Agosto;
                oImputacionNuevo.Septiembre = oImputacionModel.Septiembre;
                oImputacionNuevo.Octubre = oImputacionModel.Octubre;
                oImputacionNuevo.Noviembre = oImputacionModel.Noviembre;
                oImputacionNuevo.Diciembre = oImputacionModel.Diciembre;
                oImputacionNuevo.Activo = oImputacionModel.Activo;
                oImputacionNuevo.IdUsuario = oImputacionModel.IdUsuario;
                oImputacionNuevo.UltimaModificacion = oImputacionModel.UltimaModificacion;

                oImputacionRespuesta = ImputacionRepositorio.InsertarImputacion(oImputacionNuevo);
                if (oImputacionRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
           
        public int Eliminar(int idImputacion)
        {
            var retorno = ImputacionRepositorio.EliminarImputacion(idImputacion);
            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }

        public void ActualizarCuentaContable(CuentaPlanContableModel modelView)
        {
            try
            {
         
            switch (modelView.IdTipoElemento)
            {
                case "G":
                   
                    GrupoCuenta g = new GrupoCuenta {
                                                        Id = modelView.Codigo,
                                                        Descripcion = modelView.Descripcion,
                                                        Activo = modelView.Activo,
                                                        UltimaModificacion = DateTime.Now
                                                    };
                    g = ImputacionRepositorio.updateGrupoCuentaContable(g);
                    break;
                case "R":

                    Rubro r = new Rubro
                    {
                        Id = modelView.Codigo,
                        Descripcion = modelView.Descripcion,
                        IdGrupoCuenta = modelView.IdCuentaSuperior,
                        Activo = modelView.Activo,
                        UltimaModificacion = DateTime.Now
                    };
                    r = ImputacionRepositorio.updateRubroContable(r);
                    break;

                case "S":

                    SubRubro s = new SubRubro
                    {
                        Id = modelView.Codigo,
                        Descripcion = modelView.Descripcion,
                        IdRubro = modelView.IdCuentaSuperior,
                        Activo = modelView.Activo,
                        UltimaModificacion = DateTime.Now
                    };
                    s = ImputacionRepositorio.updateSubRubroContable(s);
                    break;

                case "C":
                    Imputacion c = new Imputacion
                    {
                        Id = modelView.Codigo,
                        Descripcion = modelView.Descripcion,
                        IdSubRubro = modelView.IdCuentaSuperior,
                        Activo = modelView.Activo,
                        UltimaModificacion = DateTime.Now
                    };
                    c = ImputacionRepositorio.updateImputacionContable(c);
                    break;

                default:
                    // code block
                    break;
            }
                _mensaje?.Invoke("Se Actualizo Correctamente", "ok");
            }
            catch (Exception ex)
            {

                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
            }
        }

        public void NuevaCuentaContable(CuentaPlanContableModel modelView)
        {
            try
            {

                switch (modelView.IdTipoElemento)
                {
                    case "G":

                        GrupoCuenta g = new GrupoCuenta
                        {
                            Id = modelView.IdNuevo,
                            Descripcion = modelView.Descripcion,
                            Activo = modelView.Activo,
                            UltimaModificacion = DateTime.Now
                        };
                        g = ImputacionRepositorio.InsertGrupoCuentaContable(g);
                        break;
                    case "R":

                        Rubro r = new Rubro
                        {
                            Id = modelView.IdNuevo,
                            Descripcion = modelView.Descripcion,
                            IdGrupoCuenta = modelView.IdCuentaSuperior,
                            Activo = modelView.Activo,
                            UltimaModificacion = DateTime.Now
                        };
                        r = ImputacionRepositorio.InsertRubroContable(r);
                        break;

                    case "S":

                        SubRubro s = new SubRubro
                        {
                            Id = modelView.IdNuevo,
                            Descripcion = modelView.Descripcion,
                            IdRubro = modelView.IdCuentaSuperior,
                            Activo = modelView.Activo,
                            UltimaModificacion = DateTime.Now
                        };
                        s = ImputacionRepositorio.InsertSubRubroContable(s);
                        break;

                    case "C":
                        Imputacion c = new Imputacion
                        {
                            Id = modelView.IdNuevo,
                            Descripcion = modelView.Descripcion,
                            IdSubRubro = modelView.IdCuentaSuperior,
                            Activo = modelView.Activo,
                            UltimaModificacion = DateTime.Now
                        };
                        c = ImputacionRepositorio.InsertImputacionContable(c);
                        break;

                    default:
                        // code block
                        break;
                }
                _mensaje?.Invoke("Se Actualizo Correctamente", "ok");
            }
            catch (Exception ex)
            {

                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
            }
        }


        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Here's how you could make a second paragraph in a description. <see cref="System.Console.WriteLine(System.String)"/> for information about output statements.</para>
        /// <seealso cref="TestClass.Main"/>
        /// </summary>

        public List<DiarioModel> GetAsientosContables(string periodo, string tipo)
        {
            try
            {
                // tipo =  a CF ó CP
                List<Diario> diario = ImputacionRepositorio.GetAsientosContables(periodo, tipo);
                List<DiarioModel> asientos = new List<DiarioModel>();
               
                switch (tipo)
                {

                    case "VF":


                        asientos = diario.GroupBy(x => new { x.IdImputacion, x.Imputacion.Descripcion })
                                .Select(c => new DiarioModel()
                                {
                                    IdImputacion = c.Key.IdImputacion,
                                    Descripcion = c.Key.Descripcion,
                                    Tipo = tipo,
                                    Periodo = periodo,
                                    Debe = c.Sum(x =>  (x.IdImputacion == 110502000 || x.IdImputacion == 110501000) ? x.Importe : 0),
                                    Haber = c.Sum(x => (x.IdImputacion != 110502000 && x.IdImputacion != 110501000) ? x.Importe : 0)
                                }).ToList();
                        break;
                    case "VP":
                        asientos = diario.GroupBy(x => new { x.IdImputacion, x.Descripcion })
                                                     .Select(c => new DiarioModel()
                                                     {
                                                         IdImputacion = c.Key.IdImputacion,
                                                         Descripcion = c.Key.Descripcion,
                                                         Tipo = tipo,
                                                         Periodo = periodo,
                                                         Debe = c.Sum(x => (x.IdImputacion != 110502000 && x.IdImputacion != 110501000) ? x.Importe : 0),
                                                         Haber = c.Sum(x => (x.IdImputacion == 110502000 || x.IdImputacion == 110501000) ? x.Importe : 0)
                                                     }).ToList();
                        break;
                    case "CP":
                        asientos = diario.GroupBy(x => new { x.IdImputacion, x.Descripcion })
                               .Select(c => new DiarioModel()
                               {
                                   IdImputacion = c.Key.IdImputacion,
                                   Descripcion = c.Key.Descripcion,
                                   Tipo = tipo,
                                   Periodo = periodo,
                                   Debe = c.Sum(x => (x.IdImputacion == 210102000) ? x.Importe : 0),
                                   Haber = c.Sum(x => (x.IdImputacion != 210102000) ? x.Importe : 0)
                               }).ToList();
                        break;
                    default:
                        tipo = (!string.IsNullOrEmpty(tipo)) ? tipo : "CF";
                        asientos = diario.GroupBy(x => new { x.IdImputacion, x.Descripcion })
                               .Select(c => new DiarioModel()
                               {
                                   IdImputacion = c.Key.IdImputacion,
                                   Descripcion = c.Key.Descripcion,
                                   Tipo = tipo,
                                   Periodo = periodo,
                                   Debe = c.Sum(x => (x.IdImputacion != 210102000) ? x.Importe : 0),
                                   Haber = c.Sum(x => (x.IdImputacion == 210102000) ? x.Importe : 0)
                               }).ToList();
                        break;
                }


                return asientos.Where(n => n.Debe != 0 || n.Haber != 0).ToList(); 

            }
            catch (Exception ex)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }
    
        public List<DiarioModel> GetAsientoContableDetalles(int cuenta, string periodo, string tipo)
        {
            try
            {
                List<Diario> diario = ImputacionRepositorio.GetAsientoContableDetalles(cuenta ,periodo, tipo);
              
                return Mapper.Map<List<Diario>, List<DiarioModel>>(diario);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public List<DiarioModel> GetCompraFactura(string periodo)
        {
            try
            {
                return Mapper.Map<List<Diario>,List<DiarioModel>>(ImputacionRepositorio.GetCompraFactura(periodo));
            }
            catch (Exception ex)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public Dictionary<int, string> ObtenerTipoAsiento()
        {
            Dictionary<int, string> diccionario = new Dictionary<int, string>();
            diccionario.Add((int)TipoAsientoEnum.CF, EnumDescription(TipoAsientoEnum.CF));
       
            return diccionario;
        }
        public Dictionary<string, string> GetTipoAsiento()
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            diccionario.Add(EnumDescriptionName(TipoAsientoEnum.CF), EnumDescription(TipoAsientoEnum.CF));
            diccionario.Add(EnumDescriptionName(TipoAsientoEnum.CP), EnumDescription(TipoAsientoEnum.CP));
            diccionario.Add(EnumDescriptionName(TipoAsientoEnum.VF), EnumDescription(TipoAsientoEnum.VF));
            diccionario.Add(EnumDescriptionName(TipoAsientoEnum.VP), EnumDescription(TipoAsientoEnum.VP));

            return diccionario;
        }

        public Dictionary<string, string> GetTipoElemento()
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            diccionario.Add(EnumDescriptionName(TipoElementoEnum.G), EnumDescription(TipoElementoEnum.G));
            diccionario.Add(EnumDescriptionName(TipoElementoEnum.R), EnumDescription(TipoElementoEnum.R));
            diccionario.Add(EnumDescriptionName(TipoElementoEnum.S), EnumDescription(TipoElementoEnum.S));
            diccionario.Add(EnumDescriptionName(TipoElementoEnum.C), EnumDescription(TipoElementoEnum.C));

            return diccionario;
        }



        public string EnumDescription(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetDescription();
        }
        public string EnumDescriptionName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

    }
}
