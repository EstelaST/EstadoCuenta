using Cuentas.Models;
using Cuentas.Options.Shared;
using System.Data;

namespace Cuentas.Repositories
{
    public class DetEstadoCuentaRepository : BaseRepository<DetEstadoCuentaTable>, IDetEstadoCuentaRepository
    {
        public DetEstadoCuentaRepository(IConfiguration config) : base(config.GetConnectionString("defaultConnection"),
                                                                             config.GetSection("DbProvider:defaultProvider").Value)
        {

        }

        public async Task<Respuesta> Send(DetEstadoCuentaTable Data, UpdateType update)
        {
            Respuesta objResultado = new Respuesta();

            try
            {
                var parametro = new List<Parametro>();

                parametro.Add(new Parametro() { ParameterName = "@TIPO_ACTUALIZA", Value = update, DbType = DbType.Int32 });
                parametro.Add(new Parametro() { ParameterName = "@IDMAEESTADOCUENTA", Value = Data.IdMaeEstadoCuenta, DbType = DbType.Int32});
                parametro.Add(new Parametro() { ParameterName = "@IDCUENTA", Value = Data.IdCuenta, DbType = DbType.Int32 });
                parametro.Add(new Parametro() { ParameterName = "@IDDETESTADOCUENTA", Value = Data.IdDetEstadoCuenta, DbType = DbType.Int32, Direction = System.Data.ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@FECHA_TRANSACCION", Value = Data.FechaTransaccion, DbType = DbType.DateTime });
                parametro.Add(new Parametro() { ParameterName = "@CONCEPTO", Value = Data.Concepto, DbType = DbType.String });
                parametro.Add(new Parametro() { ParameterName = "@CARGOS", Value = Data.Cargos, DbType = DbType.Decimal });
                parametro.Add(new Parametro() { ParameterName = "@ABONOS", Value = Data.Abonos, DbType = DbType.Decimal });

                //Gestionación de operación
                parametro.Add(new Parametro() { ParameterName = "@FILAS_AFECTADAS", Value = 0, DbType = DbType.Int32, Direction = ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@NUMERO_ERROR", Value = 0, DbType = DbType.Int32, Direction = ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@MENSAJE_ERROR", Value = "", DbType = DbType.String, Direction = ParameterDirection.InputOutput, Size = 4000 });

                await objData.ExecCmd(CommandType.StoredProcedure, "SP_MTTO_DET_ESTADO_CUENTA", true, parametro);

                if ((int)objData.objCommand.Parameters["@NUMERO_ERROR"].Value == 0)
                {
                    objResultado.Data = null;
                    objResultado.Result = true;
                    objResultado.RowsAffected = (int)objData.objCommand.Parameters["@FILAS_AFECTADAS"].Value;
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

        public async Task<Respuesta> CreateAsync(DetEstadoCuentaTable Data)
        {
            return await Send(Data, UpdateType.Add);
        }

        public async Task<Respuesta> UpdateAsync(DetEstadoCuentaTable Data)
        {
            return await Send(Data, UpdateType.Update);
        }

        public async Task<Respuesta> DeleteAsync(DetEstadoCuentaTable Data)
        {
            return await Send(Data, UpdateType.Delete);
        }

    }
}
