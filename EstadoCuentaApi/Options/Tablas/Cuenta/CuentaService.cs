using AutoMapper;
using Cuentas.Dtos;
using Cuentas.Models;
using Cuentas.Options.Shared;
using Cuentas.Repositories;

namespace Cuentas.Options.Tablas.Cuenta
{
    public class CuentaService : ICuentaService
    {
        public readonly ICuentaRepository _repo;
        public readonly IMapper _mapper;
        public CuentaService(ICuentaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Respuesta> GetDataAsync(CuentaParam Parametros)
        {
            var parametro = new List<Parametro>();
            parametro.Add(new Parametro() { ParameterName = "TIPO_CONSULTA", Value = 1, DbType = System.Data.DbType.Int32 });
            parametro.Add(new Parametro() { ParameterName = "IDUSUARIO", Value = Parametros.IdUsuario, DbType = System.Data.DbType.Int32 });
            parametro.Add(new Parametro() { ParameterName = "IDCUENTA", Value = Parametros.IdCuenta, DbType = System.Data.DbType.Int32 });
            parametro.Add(new Parametro() { ParameterName = "MES", Value = Parametros.Mes, DbType = System.Data.DbType.Int32 });
            return await _repo.GetDataAsync(parametro);
        }
        public async Task<Respuesta> CreateAsync(CuentaAddDto Data)
        {
            var CuentaTable = _mapper.Map<CuentaTable>(Data);
            return await _repo.CreateAsync(CuentaTable);
        }

        public async Task<Respuesta> UpdateAsync(CuentaUpdateDto Data)
        {
            var CuentaTable = _mapper.Map<CuentaTable>(Data);
            return await _repo.UpdateAsync(CuentaTable);
        }
        public async Task<Respuesta> DeleteAsync(CuentaDeleteDto Data)
        {
            var CuentaTable = _mapper.Map<CuentaTable>(Data);
            return await _repo.DeleteAsync(CuentaTable);
        }
    }
}
