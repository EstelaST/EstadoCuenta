using Cuentas.Dtos;
using Cuentas.Models;
using Cuentas.Options.Shared;

namespace Cuentas.Repositories
{
    public interface ICuentaRepository
    {
        Task<Respuesta> GetDataAsync(List<Parametro> Parametros);
        Task<Respuesta> CreateAsync(CuentaTable Data);
        Task<Respuesta> UpdateAsync(CuentaTable Data);
        Task<Respuesta> DeleteAsync(CuentaTable Data);
    }
}
