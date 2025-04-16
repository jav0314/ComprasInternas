using ComprasInternas.Helpers;
using ComprasInternas.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ComprasInternas.DAO
{
    public class LoginDAO
    {
        private readonly IConfiguration _config;

        public LoginDAO(IConfiguration config)
        {
            _config = config;
        }
        private SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }


        public Usuarios? ValidarLogin(string nombreUsuario, string contrasenaPlano)
        {
            using var conn = ObtenerConexion();
            using var cmd = new SqlCommand("sp_ObtenerUsuarioLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            var hashGuardado = reader["ContraseniaHash"].ToString();
            //var idRol = (int)reader["IdRol"];
            var estado = (int)reader["Estado"];

            if (estado != 1 || !AuthHelper.Verificar(contrasenaPlano, hashGuardado!))
                return null;

            return new Usuarios
            {
                IdUsuario = (int)reader["IdUsuario"],
                NombreUsuario = reader["NombreUsuario"].ToString(),
                IdRol = (int)reader["IdRol"],
                Estado = estado
            };
        }
    }
}
