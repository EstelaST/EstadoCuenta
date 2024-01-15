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
    public class DetEstadoCuentaController : Controller
    {
        private readonly IDetEstadoCuentaService _service;

        public DetEstadoCuentaController(IDetEstadoCuentaService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(DetEstadoCuentaAddDto Data)
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(DetEstadoCuentaUpdateDto Data)
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
        public async Task<IActionResult> Delete(DetEstadoCuentaDeleteDto Data)
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
