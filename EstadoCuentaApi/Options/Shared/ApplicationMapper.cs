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
        }
    }
}
