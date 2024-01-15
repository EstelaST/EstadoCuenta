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
            _data = new ControlData(config.GetSection("DbProvider:defaultProvider").Value);
        }


        //public Task<Respuesta> CreateAsync(UsuarioAddDto Data)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Respuesta> DeleteAsync(UsuarioDeleteDto Data)
        //{
        //    throw new NotImplementedException();
        //}

        public Respuesta<UsuarioLogin> LoginAsync(UsuarioLogin Data)
        {
            //Session["Usuario"/*]*/ = respuesta.Data.Usuario;

            return _data.Post<UsuarioLogin, UsuarioLogin>(Data, "", "");
        }

        //public Task<Respuesta> UpdateAsync(UsuarioUpdateDto Data)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
