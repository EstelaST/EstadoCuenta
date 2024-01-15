namespace Cuentas.Dtos
{
    public class DetEstadoCuentaAddDto
    {
        public int IdMaeEstadoCuenta { get; set; } = 0;
        public int IdCuenta { get; set; } 
        public string Concepto { get; set; }
        public decimal Cargos { get; set; }
        public decimal Abonos { get; set; }
        public DateTime? FechaTransaccion { get; set; }

    }
}
