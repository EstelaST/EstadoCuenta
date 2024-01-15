using AutoMapper;
using Cuentas.Dtos;
using Cuentas.Models;
using Cuentas.Options.Shared;
using Cuentas.Repositories;

namespace Cuentas.Services
{
    public class DetEstadoCuentaService: IDetEstadoCuentaService
    {
        public readonly IDetEstadoCuentaRepository _repo;
        public readonly IMapper _mapper;
        public DetEstadoCuentaService(IDetEstadoCuentaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Respuesta> CreateAsync(DetEstadoCuentaAddDto Data)
        {
            var DetEstadoCuentaTable = _mapper.Map<DetEstadoCuentaTable>(Data);
            return await _repo.CreateAsync(DetEstadoCuentaTable);
        }

        public async Task<Respuesta> UpdateAsync(DetEstadoCuentaUpdateDto Data)
        {
            var DetEstadoCuentaTable = _mapper.Map<DetEstadoCuentaTable>(Data);
            return await _repo.UpdateAsync(DetEstadoCuentaTable);
        }
        public async Task<Respuesta> DeleteAsync(DetEstadoCuentaDeleteDto Data)
        {
            var DetEstadoCuentaTable = _mapper.Map<DetEstadoCuentaTable>(Data);
            return await _repo.DeleteAsync(DetEstadoCuentaTable);
        }
    }
}
