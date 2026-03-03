using Microsoft.AspNetCore.Mvc;
using ProyectoArqSoft.Service;
using ProyectoArqSoft.Model.Bioquimico;
using System.Text.RegularExpressions;

namespace ProyectoArqSoft.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BioquimicoController : ControllerBase
    {
        private readonly IBioquimicoService _service;

        public BioquimicoController(IBioquimicoService service)
        {
            _service = service;
        }

        [HttpGet("buscar")]
public async Task<IActionResult> Buscar([FromQuery] string? q)
{
    try 
    {
        var criterio = q ?? string.Empty;

       
        if (!string.IsNullOrEmpty(criterio) && !Regex.IsMatch(criterio, @"^[a-zA-Z0-9]*$"))
        {
            return BadRequest(new { mensaje = "Criterio inválido." });
        }

        
        var resultados = await _service.BuscarAsync(criterio);

      
        if (resultados == null || !resultados.Any())
        {
            return Ok(new { mensaje = "No se encontraron bioquímicos." });
        }

        return Ok(resultados);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error interno: {ex.Message}");
    }
}

[HttpDelete("{id}")]
public async Task<IActionResult> Eliminar(int id)
{
    try 
    {
        var eliminado = await _service.EliminarAsync(id);

        if (!eliminado)
        {
            
            return NotFound(new { mensaje = "Bioquímico no encontrado." });
        }

        return Ok(new { mensaje = "Bioquímico eliminado exitosamente." });
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error interno: {ex.Message}");
    }
}
    }
}