using AutoMapper;
using Cuentas.Dtos;
using Cuentas.Models;
using Cuentas.Options.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Cuentas.Options.Tablas.Cuenta
{
    [ApiController]
    [Route("[controller]")]
    public class CuentaController : Controller
    {
        private readonly ICuentaService _service;
        private readonly IMapper _mapper;
        
        public CuentaController(ICuentaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Respuesta> Get([FromQuery] CuentaParam Data)
        {
            return await _service.GetDataAsync(Data);
        }
            
        [HttpPost]
        public async Task<IActionResult> Post(CuentaAddDto Data)
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
        public async Task<IActionResult> Put(CuentaUpdateDto Data)
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
        public async Task<IActionResult> Delete(CuentaDeleteDto Data)
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
