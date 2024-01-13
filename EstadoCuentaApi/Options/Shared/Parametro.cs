namespace Cuentas.Options.Shared
{
    public class Parametro
    {
        public string ParameterName { get; set; }
        public object Value { get; set; }
        public System.Data.DbType DbType { get; set; }
        public System.Data.ParameterDirection Direction { get; set; } = System.Data.ParameterDirection.Input;
        public int Size { get; set; }
    }
}
