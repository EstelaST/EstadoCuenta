using Cuentas.Dtos;
using Cuentas.Options.Shared;

namespace Cuentas.Services
{
    public interface IDetEstadoCuentaService
    {
        Task<Respuesta> CreateAsync(DetEstadoCuentaAddDto Data);
        Task<Respuesta> UpdateAsync(DetEstadoCuentaUpdateDto Data);
        Task<Respuesta> DeleteAsync(DetEstadoCuentaDeleteDto Data);
    }
}
