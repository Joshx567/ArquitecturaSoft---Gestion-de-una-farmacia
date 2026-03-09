using Microsoft.AspNetCore.Mvc;
using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Modelos.Dto;
using ProyectoArqSoft.Service;

namespace ProyectoArqSoft.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var cliente = new Cliente
                {
                    tipo_cliente = dto.tipo_cliente,
                    nombre = dto.nombre,
                    ci = dto.ci,
                    edad = dto.edad,
                    sexo = dto.sexo,
                    telefono = dto.telefono
                };

                var id = await _clienteService.CreateClienteAsync(cliente);

                return Ok(new
                {
                    id,
                    message = "Cliente creado correctamente"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clientes = await _clienteService.GetAllClientesAsync();

                if (clientes == null || clientes.Count == 0)
                    return NotFound(new { message = "No se encontraron clientes" });

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string? texto)
        {
            try
            {
                var clientes = await _clienteService.SearchClientesAsync(texto);

                if (clientes == null || clientes.Count == 0)
                    return NotFound(new { message = "No se encontraron clientes" });

                return Ok(clientes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}