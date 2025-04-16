namespace ComprasInternas.Models
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contraseña { get; set; }
        public int IdRol { get; set; }
        public int Estado { get; set; }

    }
}
