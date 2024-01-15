using Cuentas.Models;
using Cuentas.Options.Shared;

namespace Cuentas.Repositories
{
    public interface IDetEstadoCuentaRepository
    {
        Task<Respuesta> CreateAsync(DetEstadoCuentaTable Data);
        Task<Respuesta> UpdateAsync(DetEstadoCuentaTable Data);
        Task<Respuesta> DeleteAsync(DetEstadoCuentaTable Data);
    }
}
