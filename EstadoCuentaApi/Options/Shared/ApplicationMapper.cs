using AutoMapper;
using Cuentas.Dtos;
using Cuentas.Models;

namespace Cuentas.Options.Shared
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() {

            CreateMap<CuentaTable,CuentaAddDto>().ReverseMap();
            CreateMap<CuentaTable,CuentaUpdateDto>().ReverseMap();
            CreateMap<CuentaTable, CuentaDeleteDto>().ReverseMap();

            CreateMap<UsuarioTable, UsuarioAddDto>().ReverseMap();
            CreateMap<UsuarioTable, UsuarioUpdateDto>().ReverseMap();
            CreateMap<UsuarioTable, UsuarioDeleteDto>().ReverseMap();

            CreateMap<MaeEstadoCuentaTable, MaeEstadoCuentaAddDto>().ReverseMap();
            CreateMap<MaeEstadoCuentaTable, MaeEstadoCuentaUpdateDto>().ReverseMap();
            CreateMap<MaeEstadoCuentaTable, MaeEstadoCuentaDeleteDto>().ReverseMap();

            CreateMap<DetEstadoCuentaTable, DetEstadoCuentaAddDto>().ReverseMap();
            CreateMap<DetEstadoCuentaTable, DetEstadoCuentaUpdateDto>().ReverseMap();
            CreateMap<DetEstadoCuentaTable, DetEstadoCuentaDeleteDto>().ReverseMap();
        }
    }
}
