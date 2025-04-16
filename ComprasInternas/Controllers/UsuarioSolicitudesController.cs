using ComprasInternas.DAO;
using ComprasInternas.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/usuario/solicitudes")]
public class UsuarioSolicitudesController : ControllerBase
{
    private readonly SolicitudesUsuarioDAO _dao;

    public UsuarioSolicitudesController(SolicitudesUsuarioDAO dao)
    {
        _dao = dao;
    }

    // GET: api/usuario/solicitudes/{idUsuario}
    [HttpGet("{idUsuario}")]
    public IActionResult ObtenerPorUsuario(int idUsuario)
    {
        var lista = _dao.ObtenerPorUsuario(idUsuario);
        return Ok(lista);
    }

    // POST: api/usuario/solicitudes
    [HttpPost]
    public IActionResult Crear([FromBody] Solicitudes solicitud)
    {
        _dao.InsertarSolicitud(solicitud);
        return Ok(new { mensaje = "Solicitud registrada exitosamente." });
    }

    // PUT: api/usuario/solicitudes/{id}
    [HttpPut("{id}")]
    public IActionResult Editar(int id, [FromBody] Solicitudes solicitud)
    {
        var resultado = _dao.EditarSolicitud(id, solicitud);

        if (resultado)
        {
            return Ok(new { mensaje = "Solicitud actualizada." });
        }
        else
        {
            return BadRequest("No se pudo actualizar la solicitud.");
        }
    }


    // DELETE: api/usuario/solicitudes/{id}
    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id, [FromQuery] int idUsuario)
    {
        var resultado = _dao.EliminarLogicamenteSolicitud(id, idUsuario);

        if (resultado)
        {
            return Ok(new { mensaje = "Solicitud eliminada lógicamente." });
        }
        else
        {
            return BadRequest("No se pudo eliminar la solicitud.");
        }
    }

}
