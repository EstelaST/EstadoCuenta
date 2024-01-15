namespace CuentasWeb.Models
{
    public class Respuesta<T>
    {
        public decimal ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorSource { get; set; }
        public bool Result { get; set; }
        public object CodeHelper { get; set; }
        public int RowsAffected { get; set; }
        public T Data { get; set; }

        public Respuesta()
        {
            ErrorCode = 0;
            ErrorSource = "";
            ErrorMessage = "";
            RowsAffected = 0;
            CodeHelper = 0;
            Result = false;
        }
    }
}
