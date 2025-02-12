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
using System.Reflection;

namespace Negocio.Servicios
{
  public class ServicioDto : ServicioBase
    {
        private DtoRepositorio oDtoRepositorio;

        public ServicioDto()
        {
            oDtoRepositorio = kernel.Get<DtoRepositorio>();
        }

        public List<DtoModel> GetAllDto()
        {
            List<DtoModel> listaDto = Mapper.Map<List<Dto>, List<DtoModel>>(oDtoRepositorio.GetAllDto());
            return listaDto;
        }

        public DtoModel obtenerDto(int anio, int codigoDto, string codigoArticulo)
        {
            return Mapper.Map<Dto, DtoModel>(oDtoRepositorio.obtenerDto(anio, codigoDto, codigoArticulo));
        }

     
        public DtoModel Agregar(DtoModel oDtoModel)
        {
            try
            {
                var oModel = Mapper.Map<DtoModel, Dto>(oDtoModel);
                return Mapper.Map<Dto, DtoModel>(oDtoRepositorio.Agregar(oModel));
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


        public DtoModel ActualizarDatosDto (DateTime fechaFactura, ItemImprModel itemImpre,string codigoArticulo, int idTipoIva, int dto, int nFactor, int idmoneda, decimal cotizacion, ArticuloModel articuloModel, UsuarioModel oUsuario)
        {
            string fecha = fechaFactura.ToString();
            string[] FechaDesarmada = fecha.Split('/');
            string[] AnioDesarmado;
            string anio;
            string mes;
            AnioDesarmado = FechaDesarmada[2].Split(' ');
            anio = AnioDesarmado[0];

            if (cotizacion == 0 )
            {
                cotizacion = 1;
            }

            if (int.Parse(FechaDesarmada[1]) < 10)
            {
                mes = "0" + FechaDesarmada[1];
            }
            else
            {
                mes = FechaDesarmada[1];
            }

            //verifico si existe el registro a actualizar
            DtoModel dtoVerifica = Mapper.Map<Dto,DtoModel>( oDtoRepositorio.obtenerDto(int.Parse(anio), dto, codigoArticulo));

            if (dtoVerifica == null) //si no esta creado lo creo
            {
                DtoModel DtoInsertar = new DtoModel();
                DtoInsertar.Periodo = int.Parse(anio);
                DtoInsertar.CodDto = dto.ToString();
                DtoInsertar.CodArt = itemImpre.Codigo;
                DtoInsertar.Activo = true;
                DtoInsertar.IdUsuario = oUsuario.IdUsuario;
                DtoInsertar.UltimaModificacion = DateTime.Now;
                decimal valor;
                if (idTipoIva != 4) //es local
                {
                    if (idmoneda ==2) // moneda dolares
                    {
                        valor = itemImpre.Precio * nFactor;
                        switch (int.Parse(mes))
                        {
                            case 1:
                                DtoInsertar.Loc01 += valor;
                                break;
                            case 2:
                                DtoInsertar.Loc02 += valor;
                                break;
                            case 3:
                                DtoInsertar.Loc03 += valor;
                                break;
                            case 4:
                                DtoInsertar.Loc04 += valor;
                                break;
                            case 5:
                                DtoInsertar.Loc05 += valor;
                                break;
                            case 6:
                                DtoInsertar.Loc06 += valor;
                                break;
                            case 7:
                                DtoInsertar.Loc07 += valor;
                                break;
                            case 8:
                                DtoInsertar.Loc08 += valor;
                                break;
                            case 9:
                                DtoInsertar.Loc09 += valor;
                                break;
                            case 10:
                                DtoInsertar.Loc10 += valor;
                                break;
                            case 11:
                                DtoInsertar.Loc11 += valor;
                                break;
                            case 12:
                                DtoInsertar.Loc12 += valor;
                                break;
                        }
                    }
                    else //moneda pesos
                    {
                        valor = (itemImpre.Precio / cotizacion) * nFactor;

                        switch (int.Parse(mes))
                        {
                            case 1:
                                DtoInsertar.Loc01 += valor;
                                break;
                            case 2:
                                DtoInsertar.Loc02 += valor;
                                break;
                            case 3:
                                DtoInsertar.Loc03 += valor;
                                break;
                            case 4:
                                DtoInsertar.Loc04 += valor;
                                break;
                            case 5:
                                DtoInsertar.Loc05 += valor;
                                break;
                            case 6:
                                DtoInsertar.Loc06 += valor;
                                break;
                            case 7:
                                DtoInsertar.Loc07 += valor;
                                break;
                            case 8:
                                DtoInsertar.Loc08 += valor;
                                break;
                            case 9:
                                DtoInsertar.Loc09 += valor;
                                break;
                            case 10:
                                DtoInsertar.Loc10 += valor;
                                break;
                            case 11:
                                DtoInsertar.Loc11 += valor;
                                break;
                            case 12:
                                DtoInsertar.Loc12 += valor;
                                break;
                        }
                    }
                }
                else //exterior
                {
                    valor = itemImpre.Precio * nFactor;
                    switch (int.Parse(mes))
                    {
                        case 1:
                            DtoInsertar.Ext01 += valor;
                            break;
                        case 2:
                            DtoInsertar.Ext02 += valor;
                            break;
                        case 3:
                            DtoInsertar.Ext03 += valor;
                            break;
                        case 4:
                            DtoInsertar.Ext04 += valor;
                            break;
                        case 5:
                            DtoInsertar.Ext05 += valor;
                            break;
                        case 6:
                            DtoInsertar.Ext06 += valor;
                            break;
                        case 7:
                            DtoInsertar.Ext07 += valor;
                            break;
                        case 8:
                            DtoInsertar.Ext08 += valor;
                            break;
                        case 9:
                            DtoInsertar.Ext09 += valor;
                            break;
                        case 10:
                            DtoInsertar.Ext10 += valor;
                            break;
                        case 11:
                            DtoInsertar.Ext11 += valor;
                            break;
                        case 12:
                            DtoInsertar.Ext12 += valor;
                            break;
                    }
                }

                DtoModel dtoCreado = Mapper.Map<Dto, DtoModel>(oDtoRepositorio.Insertar(Mapper.Map<DtoModel,Dto> (DtoInsertar)));
                return dtoCreado;
            }
            else // esto es si ya existe el registro
            {
              ServicioDto servicioDto = new ServicioDto();


                dtoVerifica.IdUsuario = oUsuario.IdUsuario;
                dtoVerifica.UltimaModificacion = DateTime.Now;

                decimal valor;
                if (idTipoIva != 4) //es local
                {
                    if (idmoneda == 2) // moneda dolares
                    {
                        valor = itemImpre.Precio * nFactor;
                        switch (int.Parse(mes))
                        {
                            case 1:
                                dtoVerifica.Loc01 += valor;
                                break;
                            case 2:
                                dtoVerifica.Loc02 += valor;
                                break;
                            case 3:
                                dtoVerifica.Loc03 += valor;
                                break;
                            case 4:
                                dtoVerifica.Loc04 += valor;
                                break;
                            case 5:
                                dtoVerifica.Loc05 += valor;
                                break;
                            case 6:
                                dtoVerifica.Loc06 += valor;
                                break;
                            case 7:
                                dtoVerifica.Loc07 += valor;
                                break;
                            case 8:
                                dtoVerifica.Loc08 += valor;
                                break;
                            case 9:
                                dtoVerifica.Loc09 += valor;
                                break;
                            case 10:
                                dtoVerifica.Loc10 += valor;
                                break;
                            case 11:
                                dtoVerifica.Loc11 += valor;
                                break;
                            case 12:
                                dtoVerifica.Loc12 += valor;
                                break;
                        }
                    }
                    else //moneda pesos
                    {
                        valor = (itemImpre.Precio / cotizacion) * nFactor;

                        switch (int.Parse(mes))
                        {
                            case 1:
                                dtoVerifica.Loc01 += valor;
                                break;
                            case 2:
                                dtoVerifica.Loc02 += valor;
                                break;
                            case 3:
                                dtoVerifica.Loc03 += valor;
                                break;
                            case 4:
                                dtoVerifica.Loc04 += valor;
                                break;
                            case 5:
                                dtoVerifica.Loc05 += valor;
                                break;
                            case 6:
                                dtoVerifica.Loc06 += valor;
                                break;
                            case 7:
                                dtoVerifica.Loc07 += valor;
                                break;
                            case 8:
                                dtoVerifica.Loc08 += valor;
                                break;
                            case 9:
                                dtoVerifica.Loc09 += valor;
                                break;
                            case 10:
                                dtoVerifica.Loc10 += valor;
                                break;
                            case 11:
                                dtoVerifica.Loc11 += valor;
                                break;
                            case 12:
                                dtoVerifica.Loc12 += valor;
                                break;
                        }
                    }
                }
                else //exterior
                {
                    valor = itemImpre.Precio * nFactor;
                    switch (int.Parse(mes))
                    {
                        case 1:
                            dtoVerifica.Ext01 += valor;
                            break;
                        case 2:
                            dtoVerifica.Ext02 += valor;
                            break;
                        case 3:
                            dtoVerifica.Ext03 += valor;
                            break;
                        case 4:
                            dtoVerifica.Ext04 += valor;
                            break;
                        case 5:
                            dtoVerifica.Ext05 += valor;
                            break;
                        case 6:
                            dtoVerifica.Ext06 += valor;
                            break;
                        case 7:
                            dtoVerifica.Ext07 += valor;
                            break;
                        case 8:
                            dtoVerifica.Ext08 += valor;
                            break;
                        case 9:
                            dtoVerifica.Ext09 += valor;
                            break;
                        case 10:
                            dtoVerifica.Ext10 += valor;
                            break;
                        case 11:
                            dtoVerifica.Ext11 += valor;
                            break;
                        case 12:
                            dtoVerifica.Ext12 += valor;
                            break;
                    }
                }

                DtoModel dtoActuralizado = servicioDto.Actualizar(dtoVerifica);
                return dtoActuralizado;
            }

        }

     

        public DtoModel Actualizar(DtoModel oDtoModel)
        {
            try
            {
                var oModel = Mapper.Map<DtoModel, Dto>(oDtoModel);
                return Mapper.Map<Dto, DtoModel>(oDtoRepositorio.Actualizar(oModel));

            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


    }
}
