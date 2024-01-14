namespace Cuentas.Models
{
    public class UsuarioView
    {
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public int Edad { get; set; } = 0;
        public string Correo { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string Contrasena { get; set; } = "";
        public DateTime FechaCrea { get; set; }
        public DateTime FechaActualiza { get; set; }
    }
}
