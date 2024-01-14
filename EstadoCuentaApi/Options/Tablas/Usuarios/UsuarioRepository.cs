using Cuentas.Models;
using Cuentas.Options.Shared;
using DbDataReaderMapper;
using System.Data;

namespace Cuentas.Repositories
{
    public class UsuarioRepository : BaseRepository<UsuarioTable>, IUsuarioRepository
    {

        public UsuarioRepository(IConfiguration config) : base(config.GetConnectionString("defaultConnection"),
                                                                       config.GetSection("DbProvider:defaultProvider").Value)
        {

        }

        public async Task<Respuesta> GetAllDataAsync(List<Parametro> Parametros)
        {
            Respuesta objResultado = new Respuesta();

            try
            {
                var reader = await objData.GetDataReader("Usuario", Parametros);

                List<UsuarioView> ListUsuario = new List<UsuarioView>();
                while (await reader.ReadAsync())
                {
                    var response = reader.MapToObject<UsuarioView>();
                    if (response != null)
                    {
                        ListUsuario.Add(response);
                    }
                }

                reader.Close();
                reader = null;

                objResultado.Data = ListUsuario;
                objResultado.Result = true;
                objResultado.RowsAffected = ListUsuario.Count;
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


        public async Task<Respuesta> Send(UsuarioTable Data, UpdateType update)
        {
            Respuesta objResultado = new Respuesta();

            try
            {
                var parametro = new List<Parametro>();
                parametro.Add(new Parametro() { ParameterName = "@TIPO_ACTUALIZA", Value = update, DbType = DbType.Int32 });
                parametro.Add(new Parametro() { ParameterName = "@IDUSUARIO", Value = Data.IdUsuario, DbType = DbType.Int32, Direction = System.Data.ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@NOMBRE", Value = Data.Nombre, DbType = DbType.String });
                parametro.Add(new Parametro() { ParameterName = "@APELLIDO", Value = Data.Apellido, DbType = DbType.String });
                parametro.Add(new Parametro() { ParameterName = "@EDAD", Value = Data.Edad, DbType = DbType.Int32 });
                parametro.Add(new Parametro() { ParameterName = "@CORREO", Value = Data.Correo, DbType = DbType.String });
                parametro.Add(new Parametro() { ParameterName = "@USUARIO", Value = Data.Usuario, DbType = DbType.String });
                parametro.Add(new Parametro() { ParameterName = "@CONTRASENA", Value = Data.Contrasena, DbType = DbType.String });

                //Gestionación de operación
                parametro.Add(new Parametro() { ParameterName = "@FILAS_AFECTADAS", Value = 0, DbType = DbType.Int32, Direction = ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@NUMERO_ERROR", Value = 0, DbType = DbType.Int32, Direction = ParameterDirection.InputOutput });
                parametro.Add(new Parametro() { ParameterName = "@MENSAJE_ERROR", Value = "", DbType = DbType.String, Direction = ParameterDirection.InputOutput, Size = 4000 });

                await objData.ExecCmd(CommandType.StoredProcedure, "SP_MTTO_USUARIO", true, parametro);

                if ((int) objData.objCommand.Parameters["@NUMERO_ERROR"].Value == 0)
                {
                    objResultado.Data = null;
                    objResultado.Result = true;
                    objResultado.RowsAffected = 1;
                    objResultado.CodeHelper = (int) objData.objCommand.Parameters["@IDUSUARIO"].Value;
                    objResultado.ErrorCode = 0;
                    objResultado.ErrorMessage = "";
                    objResultado.ErrorSource = "";
                }
                else
                {
                    objResultado.Data = null;
                    objResultado.Result = false;
                    objResultado.RowsAffected = 0;
                    objResultado.CodeHelper = (int) objData.objCommand.Parameters["@IDUSUARIO"].Value;
                    objResultado.ErrorCode = (int) objData.objCommand.Parameters["@NUMERO_ERROR"].Value;
                    objResultado.ErrorMessage = (string) objData.objCommand.Parameters["@MENSAJE_ERROR"].Value;

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

        public async Task<Respuesta> CreateAsync(UsuarioTable Data)
        {
            return await Send(Data, UpdateType.Add);
        }

        public async Task<Respuesta> UpdateAsync(UsuarioTable Data)
        {
            return await Send(Data, UpdateType.Update);
        }

        public async Task<Respuesta> DeleteAsync(UsuarioTable Data)
        {
            return await Send(Data, UpdateType.Delete);
        }
    }
}
