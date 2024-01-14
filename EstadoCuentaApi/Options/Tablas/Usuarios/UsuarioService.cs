using AutoMapper;
using Cuentas.Dtos;
using Cuentas.Models;
using Cuentas.Options.Shared;
using Cuentas.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cuentas.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly IUsuarioRepository _repo;
        public readonly IMapper _mapper;
        public string _token; 
        public UsuarioService(IUsuarioRepository repo, IMapper mapper, IConfiguration config)
        {
            _repo = repo;
            _mapper = mapper;
            _token = config.GetValue<string>("AppSetting:Token");
        }
        public async Task<Respuesta> LoginAsync(UsuarioLogin Data)
        {
            Respuesta Respuesta = new Respuesta();

            List<Parametro> Parametro = new List<Parametro>();
            try
            {
                Parametro.Add(new Parametro() { ParameterName = "Usuario", Value = Data.Usuario, DbType = System.Data.DbType.String });

                Respuesta = await _repo.GetAllDataAsync(Parametro);

                if (Respuesta.Result == false)
                {
                    Respuesta.Data = null;
                    Respuesta.Result = false;
                    Respuesta.RowsAffected = 0;
                    Respuesta.CodeHelper = 0;
                    Respuesta.ErrorCode = -1;
                    Respuesta.ErrorMessage = "El usuario no existe";
                    Respuesta.ErrorSource = "Login()";

                    return Respuesta;
                }

                var UsuarioRespuesta = (List<UsuarioView>)Respuesta.Data;

                Data.Contrasena = EnCrypt(Data.Contrasena);

                if (Data.Contrasena != UsuarioRespuesta[0].Contrasena)
                {
                    Respuesta.Data = null;
                    Respuesta.Result = false;
                    Respuesta.RowsAffected = 0;
                    Respuesta.CodeHelper = 0;
                    Respuesta.ErrorCode = -1;
                    Respuesta.ErrorMessage = "El usuario o la contraseña son incorectos";
                    Respuesta.ErrorSource = "";

                    return Respuesta;
                }

                var token = GenerarToken(Data);
                if (!string.IsNullOrEmpty(token))
                {
                    Data.Token = token;
                    Respuesta.Data = Data;
                    Respuesta.Result = true;
                    Respuesta.RowsAffected = 0;
                    Respuesta.CodeHelper = 0;
                    Respuesta.ErrorCode = 0;
                    Respuesta.ErrorMessage = "";
                    Respuesta.ErrorSource = "";
                }else {
                    Respuesta.Data = Data;
                    Respuesta.Result = false;
                    Respuesta.RowsAffected = 0;
                    Respuesta.CodeHelper = 0;
                    Respuesta.ErrorCode = -1;
                    Respuesta.ErrorMessage = "No se pudo generar el token";
                    Respuesta.ErrorSource = "Login()";
                }

                return Respuesta;

            }
            catch (System.Exception e)
            {
                Respuesta.Data = "";
                Respuesta.Result = false;
                Respuesta.RowsAffected = 0;
                Respuesta.CodeHelper = 0;
                Respuesta.ErrorCode = -1;
                Respuesta.ErrorMessage = e.Message;
                Respuesta.ErrorSource += $"[{e.Source}]";
            }

            return Respuesta;

        }

        public async Task<Respuesta> GetAllDataAsync()
        {
            Respuesta Respuesta = new Respuesta();

            List<Parametro> Parametros = null;
            return await _repo.GetAllDataAsync(Parametros);
        }

        private async Task<bool> ExisteUsuarioAsync(string usuario) {
            Respuesta Respuesta = new Respuesta();
            
            List<Parametro> Parametro = new List<Parametro>();
            Parametro.Add(new Parametro() { ParameterName = "Usuario", Value = usuario, DbType = System.Data.DbType.String });

           Respuesta = await _repo.GetAllDataAsync(Parametro);

            return (Respuesta.RowsAffected > 0);
        }

        public async Task<Respuesta> CreateAsync(UsuarioAddDto Data)
        {
            if (await ExisteUsuarioAsync(Data.Usuario)) {
                Respuesta Respuesta = new Respuesta();
                Respuesta.Data = null;
                Respuesta.Result = false;
                Respuesta.RowsAffected = 0;
                Respuesta.CodeHelper = 0;
                Respuesta.ErrorCode = -1;
                Respuesta.ErrorMessage = "No se pudo crear porque ya existe este alguien con el mismo usuario";
                Respuesta.ErrorSource = "Create usuario";
                return Respuesta;
            }

            Data.Contrasena = EnCrypt(Data.Contrasena);
            var UsuarioTable = _mapper.Map<UsuarioTable>(Data);

            return await _repo.CreateAsync(UsuarioTable);
        }

        public async Task<Respuesta> UpdateAsync(UsuarioUpdateDto Data)
        {

            Data.Contrasena = EnCrypt(Data.Contrasena);

            var UsuarioTable = _mapper.Map<UsuarioTable>(Data);
            return await _repo.UpdateAsync(UsuarioTable);
        }
        public async Task<Respuesta> DeleteAsync(UsuarioDeleteDto Data)
        {
            var UsuarioTable = _mapper.Map<UsuarioTable>(Data);
            return await _repo.DeleteAsync(UsuarioTable);
        }

        private string EnCrypt(string Contrasena) {
            Contrasena = Encrypt(Contrasena);
            return Contrasena;
        }

        private string GenerarToken(UsuarioLogin table) {

            var ManejarToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_token);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { 
                    new Claim(ClaimTypes.Name,table.Usuario)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var Token = ManejarToken.CreateToken(tokenDescriptor);

            return ManejarToken.WriteToken(Token);
        }

        private string Encrypt(string contrasena) {

            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(contrasena);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
