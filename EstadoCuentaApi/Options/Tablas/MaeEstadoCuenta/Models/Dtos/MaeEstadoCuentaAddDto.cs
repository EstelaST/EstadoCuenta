namespace Cuentas.Dtos
{
    public class MaeEstadoCuentaAddDto
    {
        public int IdCuenta { get; set; } 
        public int Mes { get; set; } 
        public int Anio { get; set; }
        public decimal InteresBonificable { get; set; } = 0;
        public decimal PagoContado { get; set; } = 0;
        public decimal SaldoTotal { get; set; } = 0;
    }
}
