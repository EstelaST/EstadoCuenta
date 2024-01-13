using Cuentas.Options.Shared;
using Cuentas.Models;
using System.Data;
using DbDataReaderMapper;
using Cuentas.Dtos;
using AutoMapper;

namespace Cuentas.Repositories
{
    public class CuentaRepository : BaseRepository<CuentaTable>,ICuentaRepository
    {
        public CuentaRepository(IConfiguration config) :base(config.GetConnectionString("defaultConnection"),
                                                                             config.GetSection("DbProvider:defaultProvider").Value)
        {

        }

        public async Task<Respuesta>  GetDataAsync(List<Parametro> Parametros)
        {
            Respuesta objResultado = new Respuesta();

            try
            {
                var reader = await objData.GetDataReader( CommandType.StoredProcedure, "SP_DATA_ESTADO_CUENTA", Parametros);
                 // Reutilizarse 
                //ListCuenta = await CuentaMapper.ConverDataMapperAsync(reader);
                List<CuentaView> ListCuenta = new List<CuentaView>();
                while (await reader.ReadAsync())
                {
                    var response = reader.MapToObject<CuentaView>();
                    if (response != null)
                    {
                        ListCuenta.Add(response);
                    }
                }

                reader.Close();
                reader = null;
                objResultado.Data = ListCuenta;
                objResultado.Result = true;
                objResultado.RowsAffected = ListCuenta.Count;
                objResultado.CodeHelper = 0;
                objResultado.ErrorCode = 0;
                objResultado.ErrorMessage = "";
                objResultado.ErrorSource = "";

            }
            catch (System.Exception e)
            {
                objResultado.Data = null;
                objResultado.Result = false;
                objResultado.CodeHelper = 0;
                objResultado.ErrorCode = -1;
                objResultado.ErrorMessage = e.Message;
                objResultado.ErrorSource += $"[{e.Source}]";
            }
            finally
            {
                objData.objConnection.Close();
            }

            return objResultado;
        }


        public async Task<Respuesta> Send(CuentaTable Data, UpdateType update)
        {
            Respuesta objResultado = new Respuesta();

            try
            {
                var parametro = new List<Parametro>();
                parametro.Add(new Parametro() { ParameterName = "@TIPO_ACTUALIZA", Value = update, DbType = DbType.Int32 });
                parametro.Add(new Parametro() { ParameterName = "@IDCUENTA", Value = Data.IdCuenta, DbType = DbType.Int32, Direction = System.Data.ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@IDUSUARIO", Value = Data.IdUsuario, DbType = DbType.Int32 });
                parametro.Add(new Parametro() { ParameterName = "@NUMERO_CUENTA", Value = Data.NumeroCuenta, DbType = DbType.String });
                parametro.Add(new Parametro() { ParameterName = "@PORCENTAJE_INTERES", Value = Data.PorcentajeInteres, DbType = DbType.Decimal });
                parametro.Add(new Parametro() { ParameterName = "@PORCENTAJE_PAGO_MIN", Value = Data.PorcentajePagoMin, DbType = DbType.Decimal });
                parametro.Add(new Parametro() { ParameterName = "@LIMITE_CREDITO", Value = Data.LimiteCredito, DbType = DbType.Decimal });
                parametro.Add(new Parametro() { ParameterName = "@DIA_CORTE", Value = Data.DiaCorte, DbType = DbType.Int32 });
                parametro.Add(new Parametro() { ParameterName = "@PAGO_CONTADO", Value = Data.PagoContado, DbType = DbType.Decimal });
                parametro.Add(new Parametro() { ParameterName = "@SALDO_TOTAL", Value = Data.SaldoTotal, DbType = DbType.Decimal });

                //Gestionación de operación
                parametro.Add(new Parametro() { ParameterName = "@FILAS_AFECTADAS", Value = 0, DbType = DbType.Int32, Direction = ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@NUMERO_ERROR", Value = 0, DbType = DbType.Int32, Direction = ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@MENSAJE_ERROR", Value = "", DbType = DbType.String, Direction = ParameterDirection.InputOutput, Size = 4000 });

                await objData.ExecCmd( CommandType.StoredProcedure, "SP_MTTO_CUENTA", true, parametro);

                if ((int)objData.objCommand.Parameters["@NUMERO_ERROR"].Value == 0)
                {
                    objResultado.Data = null;
                    objResultado.Result = true;
                    objResultado.RowsAffected = 1;
                    objResultado.CodeHelper = (int)objData.objCommand.Parameters["@IDCUENTA"].Value;
                    objResultado.ErrorCode = 0;
                    objResultado.ErrorMessage = "";
                    objResultado.ErrorSource = "";
                }
                else
                {
                    objResultado.Data = null;
                    objResultado.Result = false;
                    objResultado.RowsAffected = 0;
                    objResultado.CodeHelper = (int)objData.objCommand.Parameters["@IDCUENTA"].Value;
                    objResultado.ErrorCode = (int)objData.objCommand.Parameters["@NUMERO_ERROR"].Value;
                    objResultado.ErrorMessage = (string)objData.objCommand.Parameters["@MENSAJE_ERROR"].Value;

                }
            }
            catch (System.Exception e)
            {
                objResultado.Data = null;
                objResultado.Result = false;
                objResultado.CodeHelper = 0;
                objResultado.ErrorCode = -1;
                objResultado.ErrorMessage = e.Message;
                objResultado.ErrorSource += $"[{e.Source}]";
            }
            finally
            {
                objData.objConnection.Close();
            }
            return objResultado;
    }

        public async Task<Respuesta> CreateAsync(CuentaTable Data)
        {
            return await Send(Data, UpdateType.Add);
        }

        public async Task<Respuesta> UpdateAsync(CuentaTable Data)
        {
            return await Send(Data, UpdateType.Update);
        }

        public async Task<Respuesta> DeleteAsync(CuentaTable Data)
        {
            return await Send(Data, UpdateType.Delete);
        }

    }
}
