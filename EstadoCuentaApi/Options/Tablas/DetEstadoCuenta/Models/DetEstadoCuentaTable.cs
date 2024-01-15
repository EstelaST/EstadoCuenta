using Cuentas.Options.Shared;

namespace Cuentas.Models
{
    public class DetEstadoCuentaTable : BaseEntity
    {
        public int IdMaeEstadoCuenta { get; set; } = 0;
        public int IdDetEstadoCuenta { get; set; } = 0;
        public int IdCuenta { get; set; } = 0;
        public string Concepto { get; set; }
        public decimal Cargos { get; set; }
        public decimal Abonos { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaTransaccion { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}
