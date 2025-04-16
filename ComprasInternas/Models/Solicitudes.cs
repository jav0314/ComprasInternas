namespace ComprasInternas.Models
{
    public class Solicitudes
    {
        public int IdSolicitud { get; set; }
        public int IdUsuario { get; set; }
        public string? Descripcion { get; set; } 
        public decimal Monto { get; set; } 
        public DateOnly FechaEsperada { get; set; }
        public string? EstadoSolicitud { get; set; }
        public int Estado { get; set; }
        public int IdSupervisor { get; set; }
        public string? Comentario { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public DateOnly FechaModificacion { get; set; }


    }
}
