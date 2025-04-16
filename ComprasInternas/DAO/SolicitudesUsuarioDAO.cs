using System.Data;
using Microsoft.Data.SqlClient;
using ComprasInternas.Models;

namespace ComprasInternas.DAO
{
    public class SolicitudesUsuarioDAO
    {
        private readonly IConfiguration _config;

        public SolicitudesUsuarioDAO(IConfiguration config)
        {
            _config = config;
        }
        private SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }
        // Obtener solicitudes por usuario
        public List<Solicitudes> ObtenerPorUsuario(int idUsuario)
        {
            var lista = new List<Solicitudes>();
            using var conn = ObtenerConexion();
            conn.Open();
            using var cmd = new SqlCommand("sp_ObtenerSolicitudesPorUsuario", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Solicitudes
                {
                    IdSolicitud = (int)reader["IdSolicitud"],
                    IdUsuario = (int)reader["IdUsuario"],
                    //NombreUsuario = reader["NombreUsuario"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    Monto = (decimal)reader["Monto"],
                    FechaEsperada = DateOnly.FromDateTime((DateTime)reader["FechaEsperada"]),
                    EstadoSolicitud = reader["EstadoSolicitud"].ToString(),
                    Estado = (int)reader["EstadoLogico"],
                    Comentario = reader["Comentario"]?.ToString(),
                    FechaRegistro = DateOnly.FromDateTime((DateTime)reader["FechaRegistro"]),
                    FechaModificacion = reader["FechaModificacion"] != DBNull.Value
    ? DateOnly.FromDateTime((DateTime)reader["FechaModificacion"])
    : DateOnly.MinValue
                });
            }

            return lista;
        }

        // Insertar nueva solicitud
        public void InsertarSolicitud(Solicitudes dto)
        {
            using var conn = ObtenerConexion();
            conn.Open();

            using var cmd = new SqlCommand("sp_InsertarSolicitud", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdUsuario", dto.IdUsuario);
            cmd.Parameters.AddWithValue("@Descripcion", dto.Descripcion);
            cmd.Parameters.AddWithValue("@Monto", dto.Monto);
            cmd.Parameters.AddWithValue("@FechaEsperada", dto.FechaEsperada);

            cmd.ExecuteNonQuery();
        }
        // Editar solicitud (solo si es del mismo usuario y está pendiente)
        public bool EditarSolicitud(int id, Solicitudes dto)
        {
            using var conn = ObtenerConexion();
            conn.Open();

            using var cmd = new SqlCommand("sp_EditarSolicitud", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdSolicitud", id);
            cmd.Parameters.AddWithValue("@IdUsuario", dto.IdUsuario);
            cmd.Parameters.AddWithValue("@NuevaDescripcion", dto.Descripcion);
            cmd.Parameters.AddWithValue("@NuevoMonto", dto.Monto);
            cmd.Parameters.AddWithValue("@NuevaFechaEsperada", dto.FechaEsperada);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
        // Eliminar lógicamente
        public bool EliminarLogicamenteSolicitud(int idSolicitud, int idUsuario)
        {
            using var conn = ObtenerConexion();
            conn.Open();

            using var cmd = new SqlCommand("sp_EliminarLogicoSolicitud", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdSolicitud", idSolicitud);
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

    }
}
