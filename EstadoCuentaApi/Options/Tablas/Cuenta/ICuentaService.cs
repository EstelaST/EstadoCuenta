using Cuentas.Dtos;
using Cuentas.Options.Shared;
using Cuentas.Models;

namespace Cuentas.Options.Tablas.Cuenta
{
    public interface ICuentaService
    {
        Task<Respuesta> GetDataAsync(CuentaParam Parametros);
        Task<Respuesta> CreateAsync(CuentaAddDto Data);
        Task<Respuesta> UpdateAsync(CuentaUpdateDto Data);
        Task<Respuesta> DeleteAsync(CuentaDeleteDto Data);
    }
}
