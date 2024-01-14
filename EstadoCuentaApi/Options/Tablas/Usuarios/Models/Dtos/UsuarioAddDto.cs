namespace Cuentas.Dtos
{
    public class UsuarioAddDto
    {
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public int Edad { get; set; } = 0;
        public string Correo { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string Contrasena { get; set; } = "";
    }
}
