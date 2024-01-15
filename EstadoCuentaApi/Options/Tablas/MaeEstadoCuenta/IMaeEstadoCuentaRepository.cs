using Cuentas.Models;
using Cuentas.Options.Shared;

namespace Cuentas.Repositories
{
    public interface IMaeEstadoCuentaRepository
    {
        Task<Respuesta> CreateAsync(MaeEstadoCuentaTable Data);
        Task<Respuesta> UpdateAsync(MaeEstadoCuentaTable Data);
        Task<Respuesta> DeleteAsync(MaeEstadoCuentaTable Data);
    }
}
