namespace Cuentas.Models
{
    public class CuentaView
    {
        public string Nombre { get; set; } = "";
        public string Usuario { get; set; } = "";   
        public string Correo { get; set; } = "";
        public DateTime FechaApertura { get; set; }
        public string NumeroTarjeta { get; set; } = "";
        public decimal LimiteCredito { get; set; } = 0;
        public decimal PagoMinimo { get; set; } = 0;
        public decimal SaldoDisponible { get; set; } = 0;
        public decimal InteresBonificable { get; set; } = 0;
        public decimal PagoContado { get; set; } = 0;
        public decimal SaldoActual { get; set; } = 0;
    }
}
