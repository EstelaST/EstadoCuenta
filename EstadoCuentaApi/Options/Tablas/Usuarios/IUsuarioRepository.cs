using Cuentas.Models;
using Cuentas.Options.Shared;

namespace Cuentas.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Respuesta> GetAllDataAsync(List<Parametro> Parametros);
        Task<Respuesta> CreateAsync(UsuarioTable Data);
        Task<Respuesta> UpdateAsync(UsuarioTable Data);
        Task<Respuesta> DeleteAsync(UsuarioTable Data);
    }
}
