namespace Cuentas.Dtos
{
    public class DetEstadoCuentaUpdateDto
    {
        public int IdMaeEstadoCuenta { get; set; }
        public int IdDetEstadoCuenta { get; set; }
        public string Concepto { get; set; }
        public decimal Cargos { get; set; }
        public decimal Abonos { get; set; }
        public DateTime? FechaTransaccion { get; set; }

    }
}
