using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
    public class DtoRepositorio :RepositorioBase <Dto>
    {

        private SAC_Entities context;
        public DtoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public List<Dto> GetAllDto()
        {
            List<Dto> listaDto = context.Dto.Where(p => p.Activo == true).ToList();
            return listaDto;
        }


        public Dto obtenerDto(int anio, int CodigoDto, string CodigoArt)
        {
            var retorno =  context.Dto.Where(p => p.Periodo == anio && p.CodArt == CodigoArt && p.CodDto == CodigoDto.ToString()).FirstOrDefault();
            return retorno;
        }


        public Dto Agregar(Dto oDto)
        {
            return Insertar(oDto);
        }

        public Dto Actualizar(Dto oDto)
        {
            Dto nDto = obtenerDto(oDto.Periodo, int.Parse(oDto.CodDto), oDto.CodArt);
            nDto.Id = oDto.Id;
            nDto.Periodo = oDto.Periodo;
            nDto.CodDto = oDto.CodDto;
            nDto.CodArt = oDto.CodArt;
            nDto.NomDto = oDto.NomDto;
            nDto.Loc01 = oDto.Loc01;
            nDto.Loc02 = oDto.Loc02;
            nDto.Loc03 = oDto.Loc03;
            nDto.Loc04 = oDto.Loc04;
            nDto.Loc05 = oDto.Loc05;
            nDto.Loc06 = oDto.Loc06;
            nDto.Loc07 = oDto.Loc07;
            nDto.Loc08 = oDto.Loc08; 
            nDto.Loc09 = oDto.Loc09;
            nDto.Loc10 = oDto.Loc10;
            nDto.Loc11 = oDto.Loc11;
            nDto.Loc12 = oDto.Loc12;
            nDto.Ext01 = oDto.Ext01;
            nDto.Ext02 = oDto.Ext02;
            nDto.Ext03 = oDto.Ext03;
            nDto.Ext04 = oDto.Ext04;
            nDto.Ext05 = oDto.Ext05;
            nDto.Ext06 = oDto.Ext06;
            nDto.Ext07 = oDto.Ext07;
            nDto.Ext08 = oDto.Ext08;
            nDto.Ext09 = oDto.Ext09;
            nDto.Ext10 = oDto.Ext10;
            nDto.Ext11 = oDto.Ext11;
            nDto.Ext12 = oDto.Ext12;
            nDto.Activo = oDto.Activo;
            nDto.IdUsuario = oDto.IdUsuario;
            nDto.UltimaModificacion = oDto.UltimaModificacion;
            context.SaveChanges();
            return nDto;
        }


    }
}
