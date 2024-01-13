using AutoMapper;
using DbDataReaderMapper;
using System.Data.Common;

namespace Cuentas.Options.Shared
{
    public  class CuentaMapper
    {
        public  async Task<List<T>> ConverDataMapperAsync<T>(DbDataReader dr) where T : class
        {
            List<T> ListCuenta = new List<T>();
            while (await dr.ReadAsync())
            {
                var response = dr.MapToObject<T>();
                if (response != null)
                {
                    ListCuenta.Add(response);

                }
            }
            return ListCuenta;
        }
    }
}
