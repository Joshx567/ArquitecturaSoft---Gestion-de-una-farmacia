using Microsoft.AspNetCore.Mvc;
using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Modelos.Dto;
using ProyectoArqSoft.Service;

namespace ProyectoArqSoft.Controllers
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

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("ok");

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBioquimicoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bio = new Bioquimico
            {
                nombres = dto.nombres,
                apellidos = dto.apellidos,
                ci = dto.ci,
                telefono = dto.telefono,
                activo = dto.activo
            };

            var (ok, mensaje) = await _service.CrearAsync(bio);
            if (!ok) return BadRequest(new { error = mensaje });

            return Ok(new { message = "Bioquímico registrado correctamente" });
        }
    }
}