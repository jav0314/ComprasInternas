using ComprasInternas.DAO;
using ComprasInternas.Models;
using Microsoft.AspNetCore.Mvc;

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
        var usuario = _dao.ValidarLogin(cred.NombreUsuario ?? "", cred.Contraseña ?? "");

        if (usuario == null)
            return Unauthorized("Credenciales inválidas");

        return Ok(usuario);
    }
}
