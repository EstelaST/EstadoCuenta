using Cuentas.Options.Shared;

namespace Cuentas.Dtos
{
    public class CuentaDeleteDto : BaseEntity
    {
        public int IdCuenta { get; set; } 
        public int IdUsuario { get; set; }
    }
}