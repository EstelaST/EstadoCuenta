﻿using Cuentas.Dtos;
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
    public class CuentaController : Controller
    {
        private readonly ICuentaService _service;
        
        public CuentaController(ICuentaService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<Respuesta> Get([FromQuery] CuentaParam Data)
        {
            return await _service.GetDataAsync(Data);
        }
            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
