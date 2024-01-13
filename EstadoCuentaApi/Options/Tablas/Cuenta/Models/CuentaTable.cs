using Cuentas.Options.Shared;

namespace Cuentas.Models
{
    public class CuentaTable : BaseEntity
    {
        public int IdCuenta { get; set; } = 0;
        public int IdUsuario { get; set; } = 0;
        public string NumeroCuenta { get; set; } = "";
        public decimal PorcentajeInteres { get; set; } = 0;
        public decimal PorcentajePagoMin { get; set; } = 0;
        public decimal LimiteCredito { get; set; } = 0;
        public int DiaCorte { get; set; } = 0;  
        public decimal PagoContado { get; set; } = 0;
        public decimal SaldoTotal { get; set; } = 0;
        public DateTime FechaCrea { get; set; }
        public DateTime FechaActualiza { get; set; }
    }
}
