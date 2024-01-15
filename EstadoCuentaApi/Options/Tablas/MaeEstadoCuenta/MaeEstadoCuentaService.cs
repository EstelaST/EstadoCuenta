using AutoMapper;
using Cuentas.Dtos;
using Cuentas.Models;
using Cuentas.Options.Shared;
using Cuentas.Repositories;

namespace Cuentas.Services
{
    public class MaeEstadoCuentaService : IMaeEstadoCuentaService
    {
        public readonly IMaeEstadoCuentaRepository _repo;
        public readonly IMapper _mapper;
        public MaeEstadoCuentaService(IMaeEstadoCuentaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Respuesta> CreateAsync(MaeEstadoCuentaAddDto Data)
        {
            var MaeEstadoCuentaTable = _mapper.Map<MaeEstadoCuentaTable>(Data);
            return await _repo.CreateAsync(MaeEstadoCuentaTable);
        }

        public async Task<Respuesta> UpdateAsync(MaeEstadoCuentaUpdateDto Data)
        {
            var MaeEstadoCuentaTable = _mapper.Map<MaeEstadoCuentaTable>(Data);
            return await _repo.UpdateAsync(MaeEstadoCuentaTable);
        }
        public async Task<Respuesta> DeleteAsync(MaeEstadoCuentaDeleteDto Data)
        {
            var MaeEstadoCuentaTable = _mapper.Map<MaeEstadoCuentaTable>(Data);
            return await _repo.DeleteAsync(MaeEstadoCuentaTable);
        }
    }
}
