using ComprasInternas.DAO;
using ComprasInternas.Models;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/auth")]
public class RegisterController : ControllerBase
{
    private readonly RegisterDAO _dao;

    public RegisterController(RegisterDAO dao)
    {
        _dao = dao;
    }

    [HttpPost("register")]
    public IActionResult Registrar([FromBody] Usuarios usuario)
    {
        if (usuario == null ||
       string.IsNullOrWhiteSpace(usuario.NombreUsuario) ||
       string.IsNullOrWhiteSpace(usuario.Contraseña) ||
       usuario.IdRol <= 0)
        {
            return BadRequest("Datos inválidos.");
        }

        var exito = _dao.RegistrarUsuario(usuario, out string mensaje);

        if (exito)
            return Ok("Usuario registrado exitosamente.");
        else
            return StatusCode(500, mensaje);
    }

}
