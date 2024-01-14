using Cuentas.Dtos;
using Cuentas.Models;
using Cuentas.Options.Shared;

namespace Cuentas.Services
{
    public interface IUsuarioService
    {
        Task<Respuesta> GetAllDataAsync();
        Task<Respuesta> CreateAsync(UsuarioAddDto Data);
        Task<Respuesta> UpdateAsync(UsuarioUpdateDto Data);
        Task<Respuesta> DeleteAsync(UsuarioDeleteDto Data);
        Task<Respuesta> LoginAsync(UsuarioLogin Data);
    }
}
