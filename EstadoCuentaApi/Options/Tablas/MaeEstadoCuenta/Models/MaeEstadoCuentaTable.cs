using Cuentas.Options.Shared;

namespace Cuentas.Models
{
    public class MaeEstadoCuentaTable : BaseEntity
    {
        public int IdMaeEstadoCuenta { get; set; } = 0;
        public int IdCuenta { get; set; } = 0;
        public int Mes { get; set; } = 0;
        public int Anio { get; set; } = 0;
        public decimal InteresBonificable { get; set; } = 0;
        public decimal PagoContado { get; set; } = 0;
        public decimal SaldoTotal { get; set; } = 0;
        public DateTime? FechaCorte { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaActualiza { get; set; }

    }
}
