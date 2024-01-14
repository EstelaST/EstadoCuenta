
using AutoMapper;

namespace Cuentas.Options.Shared
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public ControlData objData;
        public IMapper _mapper;
        protected BaseRepository(string connectionString, string providerName)
        {
            objData = new ControlData(connectionString, providerName);
        }
    }
}
