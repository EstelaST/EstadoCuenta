using Cuentas.Options.Shared;

namespace Cuentas.Dtos
{
    public class CuentaUpdateDto : BaseEntity
    {
        public int IdCuenta { get; set; } 
        public int IdUsuario { get; set; }
        public string NumeroCuenta { get; set; } = "";
        public decimal PorcentajeInteres { get; set; } = 0;
        public decimal PorcentajePagoMin { get; set; } = 0;
        public decimal LimiteCredito { get; set; } = 0;
        public int DiaCorte { get; set; } = 0;
        public decimal PagoContado { get; set; } = 0;
        public decimal SaldoTotal { get; set; } = 0;
    }
}