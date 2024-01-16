using CuentasWeb.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CuentasWeb.Shared
{
    public class ControlData
    {
        public string UrlAPI { get; set; }
        public string Token { get; set; } = "";

        private HttpClient objClient = new HttpClient();

        public ControlData(string xUrlAPI)
        {
            UrlAPI = xUrlAPI;
            objClient.DefaultRequestHeaders.Accept.Clear();
            objClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            objClient.Timeout = TimeSpan.FromMinutes(1);
        }

        public async Task<Respuesta<TData>> GetAsync<TData>(string xController, string xMetodo, IDictionary<string, object> xQueryList = null)
        {
            Respuesta<TData> objResultado;
            try
            {
                if (Token != "") { objClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token); }

                UriBuilder builder = new UriBuilder(UrlAPI + xController + "/" + xMetodo);
                if (xQueryList.Count > 0)
                {
                    builder.Query = getQuery(xQueryList);
                }

                HttpResponseMessage vResponse = await objClient.GetAsync(builder.Uri).ConfigureAwait(false);
                objResultado = JsonConvert.DeserializeObject<Respuesta<TData>>(vResponse.Content.ReadAsStringAsync().Result);

                if (objResultado != null)
                {
                    return objResultado;
                }

                objResultado = new Respuesta<TData>();
                objResultado.Result = false;
                objResultado.ErrorCode = -1;
                objResultado.CodeHelper = 0;
                objResultado.RowsAffected = 0;
                objResultado.ErrorMessage = $"{vResponse.StatusCode.ToString()}";

                return objResultado;

            }
            catch (Exception e)
            {
                objResultado = new Respuesta<TData>();
                objResultado.Result = false;
                objResultado.ErrorCode = -1;
                objResultado.CodeHelper = 0;
                objResultado.RowsAffected = 0;
                objResultado.ErrorSource = e.Source;
                objResultado.ErrorMessage = e.Message;

                return objResultado;
            }
        }

        public Respuesta<TData> Put<TContent, TData>(TContent xContent, string xController, string xMetodo, IDictionary<string, object> xQueryList = null)
        {
            Respuesta<TData> objResultado;
            try
            {
                if (Token != "") { objClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token); }

                string objSerilized = JsonConvert.SerializeObject(xContent);

                UriBuilder builder = new UriBuilder(UrlAPI + xController + "/" + xMetodo);
                if (xQueryList != null)
                {
                    builder.Query = getQuery(xQueryList);
                }

                HttpResponseMessage vResponse = objClient.PutAsync(builder.Uri, new StringContent(objSerilized, Encoding.UTF8, "application/json")).Result;
                objResultado = JsonConvert.DeserializeObject<Respuesta<TData>>(vResponse.Content.ReadAsStringAsync().Result);

                if (objResultado != null)
                {
                    return objResultado;
                }

                objResultado = new Respuesta<TData>();
                objResultado.Result = false;
                objResultado.ErrorCode = -1;
                objResultado.CodeHelper = 0;
                objResultado.RowsAffected = 0;
                objResultado.ErrorMessage = $"{vResponse.StatusCode.ToString()}";

                return objResultado;
            }
            catch (Exception e)
            {
                objResultado = new Respuesta<TData>();
                objResultado.Result = false;
                objResultado.ErrorCode = -1;
                objResultado.CodeHelper = 0;
                objResultado.RowsAffected = 0;
                objResultado.ErrorSource = e.Source;
                objResultado.ErrorMessage = e.Message;

                return objResultado;
            }
        }

        private string getQuery(IDictionary<string, object> xQueryList)
        {
            string vQuery = "";

            foreach (var item in xQueryList)
            {
                if (vQuery != "") { vQuery += "&"; }
                if (item.Value is DateTime)
                {
                    vQuery += $"{item.Key}={((DateTime)item.Value).ToString("yyyy-MM-ddTHH:mm:ss")}";
                }
                else
                {
                    var vValue = "";
                    if (item.Value == null)
                    {
                        vValue = "null";
                    }
                    else
                    {
                        vValue = item.Value.ToString();
                    }
                    vQuery += $"{item.Key}={vValue}";
                }
            }

            return vQuery;
        }

    }
}
