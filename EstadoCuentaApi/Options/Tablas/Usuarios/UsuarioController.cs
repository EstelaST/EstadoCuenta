using Cuentas.Dtos;
using Cuentas.Models;
using Cuentas.Options.Shared;
using Cuentas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cuentas.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<Respuesta> Get()
        {
            return await _service.GetAllDataAsync();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(UsuarioAddDto Data)
        {
            var resultado = await _service.CreateAsync(Data);
            if (resultado.ErrorCode == 0)
            {
                return StatusCode(201, resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioLogin Data)
        {
            var resultado = await _service.LoginAsync(Data);

            if (!resultado.Result)
            {
                return BadRequest(resultado);
            }
            else
            {
                var usuario = (UsuarioLogin)resultado.Data;
                if (string.IsNullOrEmpty(usuario.Token))
                {
                    return BadRequest(resultado);
                }
            }
            return Ok(resultado);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(UsuarioUpdateDto Data)
        {
            var resultado = await _service.UpdateAsync(Data);
            if (resultado.ErrorCode == 0)
            {
                return StatusCode(201, resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(UsuarioDeleteDto Data)
        {
            var resultado = await _service.DeleteAsync(Data);
            if (resultado.ErrorCode == 0)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }
    }
}
