using ComprasInternas.Helpers;
using ComprasInternas.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ComprasInternas.DAO
{
    public class RegisterDAO
    {
        private readonly IConfiguration _config;

        public RegisterDAO(IConfiguration config)
        {
            _config = config;
        }
        private SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public bool RegistrarUsuario(Usuarios usuario, out string mensaje)
        {
            mensaje = string.Empty;
            using var conn = ObtenerConexion();
            try
            {
                string hash = AuthHelper.Hash(usuario.Contraseña);
                using var cmd = new SqlCommand("sp_RegistrarUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NombreUsuario", SqlDbType.NVarChar, 100).Value = usuario.NombreUsuario;
                cmd.Parameters.Add("@ContraseniaHash", SqlDbType.NVarChar, 255).Value = hash;
                cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;

                conn.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (SqlException sqlEx)
            {
                mensaje = "Error en base de datos: " + sqlEx.Message;
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado: " + ex.Message;
                return false;
            }
        }
    }
}
