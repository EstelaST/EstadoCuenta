using Cuentas.Dtos;
using Cuentas.Options.Shared;

namespace Cuentas.Services
{
    public interface IMaeEstadoCuentaService
    {
        Task<Respuesta> CreateAsync(MaeEstadoCuentaAddDto Data);
        Task<Respuesta> UpdateAsync(MaeEstadoCuentaUpdateDto Data);
        Task<Respuesta> DeleteAsync(MaeEstadoCuentaDeleteDto Data);
    }
}
