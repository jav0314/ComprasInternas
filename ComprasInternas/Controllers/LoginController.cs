using ComprasInternas.DAO;
using ComprasInternas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/auth")]
public class LoginController : ControllerBase
{
    private readonly LoginDAO _dao;
    private readonly IConfiguration _config;
    public LoginController(LoginDAO dao, IConfiguration config)
    {
        _dao = dao;
        _config = config;
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] Usuarios cred)
    {
        // Simulación de validación (reemplaza con lógica real)
        var usuario = _dao.ValidarLogin(cred.NombreUsuario ?? "", cred.Contraseña ?? "");

        if (usuario == null)
            return Unauthorized("Credenciales inválidas");

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
        new Claim(ClaimTypes.Name, usuario.NombreUsuario!),
        new Claim(ClaimTypes.Role, usuario.IdRol == 2 ? "Supervisor" : "Usuario")
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
