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

        if (resultado)
        {
            return Ok(new { mensaje = "Solicitud aprobada." });
        }
        else
        {
            return BadRequest(new { error = "Error al aprobar." });
        }
    }


    // PUT: api/supervisor/solicitudes/{id}/rechazar
    [HttpPut("{id}/rechazar")]
    public IActionResult Rechazar(int id, [FromBody] Solicitudes dto)
    {
        var resultado = _dao.RechazarSolicitud(id, dto);

        if (resultado)
        {
            return Ok(new { mensaje = "Solicitud rechazada." });
        }
        else
        {
            return BadRequest(new { error = "Error al rechazar." });
        }
    }

    [HttpGet("solicitudes/aprobadas")]
    public IActionResult ObtenerSolicitudesAprobadas()
    {
        var aprobadas = _dao.ObtenerSolicitudesAprobadas();
        return Ok(aprobadas);
    }


}
