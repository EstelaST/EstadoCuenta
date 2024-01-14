using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace Cuentas.Options.Shared
{
    public class ControlData
    {
        private string _connectiongString;
        private DbProviderFactory dataFactory;
        public DbCommand objCommand { get; set; }
        private DbParameter objParameter;
        public DbConnection objConnection;
        private DbDataReader objReader;
        private SqlBuilder objSQLBuilder;

        public ControlData(string connectionString, string providerName)
        {
            _connectiongString = connectionString;
            DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);
            dataFactory = DbProviderFactories.GetFactory(providerName);

            objConnection = dataFactory.CreateConnection();
            objConnection.ConnectionString = _connectiongString;
            objSQLBuilder = new SqlBuilder();
        }

        public async Task<DbDataReader> GetDataReader(string vViewName, List<Parametro> xParametros = null)
        {
            try
            {
                string xQry = "";
                string vWhere = "";

                if (objConnection.State != ConnectionState.Open)
                    await objConnection.OpenAsync();

                objCommand = dataFactory.CreateCommand();
                objCommand.Connection = objConnection;
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandTimeout = 0;

                if (xParametros != null)
                {
                    foreach (Parametro p in xParametros)
                    {
                        if (p.ParameterName != "")
                        {
                            objParameter = dataFactory.CreateParameter();
                            objParameter.DbType = p.DbType;
                            objParameter.ParameterName = p.ParameterName;
                            objParameter.Value = p.Value;
                            objParameter.Direction = p.Direction;
                            objParameter.Size = p.Size;

                            objCommand.Parameters.Add(objParameter);

                            if (vWhere != "") { vWhere += " AND "; }
                            vWhere += $"{p.ParameterName}=@{p.ParameterName}";
                        }
                    }
                }

                xQry = objSQLBuilder.toSelect(vViewName, vWhere);

                objCommand.CommandText = xQry;

                objReader = await objCommand.ExecuteReaderAsync();

            }
            catch (System.Exception)
            {
                throw;
            }

            return objReader;
        }

        public async Task<DbDataReader> GetDataReader(CommandType xCommandType,
                                          string xQry = "",
                                          List<Parametro> xParametros = null)
        {
            try
            {
                if (objConnection.State != ConnectionState.Open)
                    await objConnection.OpenAsync();

                objCommand = dataFactory.CreateCommand();
                objCommand.Connection = objConnection;
                objCommand.CommandType = xCommandType;
                objCommand.CommandTimeout = 0;

                if (xParametros != null)
                {
                    foreach (Parametro p in xParametros)
                    {
                        if (p.ParameterName != "")
                        {
                            objParameter = dataFactory.CreateParameter();
                            objParameter.DbType = p.DbType;
                            objParameter.ParameterName = p.ParameterName;
                            objParameter.Value = p.Value;
                            objParameter.Direction = p.Direction;
                            objParameter.Size = p.Size;

                            objCommand.Parameters.Add(objParameter);
                        }
                    }
                }

                objCommand.CommandText = xQry;

                objReader = await objCommand.ExecuteReaderAsync();

            }
            catch (System.Exception)
            {
                throw;
            }

            return objReader;
        }

        public async Task<object> ExecCmd(CommandType xCommandType,
                                          string xQry,
                                          bool xRowsAfected = true,
                                          List<Parametro> xParametros = null)
        {
            object sqlrows = 0;

            try
            {
                if (objConnection.State != ConnectionState.Open)
                    await objConnection.OpenAsync();

                objCommand = dataFactory.CreateCommand();
                objCommand.Connection = objConnection;
                objCommand.CommandType = xCommandType;
                objCommand.CommandText = xQry;
                objCommand.CommandTimeout = 0;

                if (xParametros != null)
                {
                    foreach (Parametro p in xParametros)
                    {
                        if (p.ParameterName != "")
                        {
                            objParameter = dataFactory.CreateParameter();
                            objParameter.DbType = p.DbType;
                            objParameter.ParameterName = p.ParameterName;
                            objParameter.Value = p.Value;
                            objParameter.Direction = p.Direction;
                            objParameter.Size = p.Size;

                            objCommand.Parameters.Add(objParameter);
                        }
                    }
                }

                if (xRowsAfected)
                {
                    sqlrows = await objCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    sqlrows = await objCommand.ExecuteScalarAsync();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                objConnection.Close();
            }
            return sqlrows;
        }

    }
}
