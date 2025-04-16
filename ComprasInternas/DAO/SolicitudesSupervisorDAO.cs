using System.Data;
using Microsoft.Data.SqlClient;
using ComprasInternas.Models;


namespace ApiComprasInternas.DAO;

public class SolicitudSupervisorDAO
{
    private readonly IConfiguration _config;

    public SolicitudSupervisorDAO(IConfiguration config)
    {
        _config = config;
    }

    private SqlConnection ObtenerConexion()
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    }

    // Obtener todas las solicitudes (para el supervisor)
    public List<Solicitudes> ObtenerTodas()
    {
        var lista = new List<Solicitudes>();
        using var conn = ObtenerConexion();
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM vw_SolicitudesPorUsuario", conn); //No es lo más óptimo ni seguro, por lo que hay que buscar otra manera
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

    // Aprobar o rechazar solicitud
    public bool AprobarSolicitud(int idSolicitud, Solicitudes dto)
    {
        return AprobarORechazar(2, idSolicitud, dto);
    }

    public bool RechazarSolicitud(int idSolicitud, Solicitudes dto)
    {
        return AprobarORechazar(3, idSolicitud, dto);
    }

    private bool AprobarORechazar(int tipo, int idSolicitud, Solicitudes dto)
    {
        using var conn = ObtenerConexion();
        conn.Open();

        using var cmd = new SqlCommand("sp_AprobarRechazarSolicitud", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdSolicitud", idSolicitud);
        cmd.Parameters.AddWithValue("@IdSupervisor", 2);
        cmd.Parameters.AddWithValue("@NuevoEstadoSolicitud", tipo);
        cmd.Parameters.AddWithValue("@Comentario", dto.Comentario ?? string.Empty);

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
    public List<Solicitudes> ObtenerSolicitudesAprobadas()
    {
        var solicitudes = new List<Solicitudes>();

        using var conn = ObtenerConexion();
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM vw_SolicitudesAprobadasSupervisor", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            solicitudes.Add(new Solicitudes
            {
                IdSolicitud = reader.GetInt32(0),
                IdUsuario = reader.GetInt32(1),
                Descripcion = reader.GetString(3),
                Monto = reader.GetDecimal(4),
                FechaEsperada = DateOnly.FromDateTime(reader.GetDateTime(5)),
                EstadoSolicitud = reader.GetString(6),
                Comentario = reader.IsDBNull(10) ? "" : reader.GetString(10),
                FechaRegistro = DateOnly.FromDateTime(reader.GetDateTime(11)),
                FechaModificacion = DateOnly.FromDateTime(reader.GetDateTime(12))
            });
        }

        return solicitudes;
    }

}
