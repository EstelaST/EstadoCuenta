using CuentasWeb.Dtos;
using CuentasWeb.Models;
using CuentasWeb.Shared;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text;

namespace CuentasWeb.Services
{
    public class UsuarioService : IUsuarioService
    {

        public ControlData _data;
        public UsuarioService(IConfiguration config)
        {
            _data = new ControlData(config.GetSection("Server:CuentasServer").Value);
        }
        public Respuesta<UsuarioLogin> LoginAsync(UsuarioLogin Data)
        {

            return _data.Put<UsuarioLogin, UsuarioLogin>(Data, "", "");
        }

        public Respuesta<UsuarioAddDto> CreateAsync(UsuarioAddDto Data)
        {
            return _data.Put<UsuarioAddDto, UsuarioAddDto>(Data, "", "");
        }

        public Respuesta<UsuarioUpdateDto> UpdateAsync(UsuarioUpdateDto Data)
        {
            return _data.Put<UsuarioUpdateDto, UsuarioUpdateDto>(Data, "", "");
        }

        public Respuesta<UsuarioDeleteDto> DeleteAsync(UsuarioDeleteDto Data)
        {
            return _data.Put<UsuarioDeleteDto, UsuarioDeleteDto>(Data, "", "");
        }

    }
}
