using ApiComprasInternas.DAO;
using ComprasInternas.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/supervisor/solicitudes")]
public class SupervisorSolicitudesController : ControllerBase
{
    private readonly SolicitudSupervisorDAO _dao;

    public SupervisorSolicitudesController(SolicitudSupervisorDAO dao)
    {
        _dao = dao;
    }

    // GET: api/supervisor/solicitudes
    [HttpGet]
    public IActionResult ObtenerTodas()
    {
        var lista = _dao.ObtenerTodas();
        return Ok(lista);
    }

    // PUT: api/supervisor/solicitudes/{id}/aprobar
    [HttpPut("{id}/aprobar")]
    public IActionResult Aprobar(int id, [FromBody] Solicitudes dto)
    {
        var resultado = _dao.AprobarSolicitud(id, dto);
        return resultado ? Ok("Solicitud aprobada.") : BadRequest("Error al aprobar.");
    }

    // PUT: api/supervisor/solicitudes/{id}/rechazar
    [HttpPut("{id}/rechazar")]
    public IActionResult Rechazar(int id, [FromBody] Solicitudes dto)
    {
        var resultado = _dao.RechazarSolicitud(id, dto);
        return resultado ? Ok("Solicitud rechazada.") : BadRequest("Error al rechazar.");
    }
}
