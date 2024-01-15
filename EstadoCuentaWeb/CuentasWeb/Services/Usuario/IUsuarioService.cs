using CuentasWeb.Dtos;
using CuentasWeb.Models;
using CuentasWeb.Models.Usuario;

namespace CuentasWeb.Services
{
    public interface IUsuarioService
    {
        //Respuesta<> CreateAsync(UsuarioAddDto Data);
        //Respuesta<> UpdateAsync(UsuarioUpdateDto Data);
        //Respuesta<> DeleteAsync(UsuarioDeleteDto Data);
        Respuesta<UsuarioLogin> LoginAsync(UsuarioLogin Data);
    }
}
