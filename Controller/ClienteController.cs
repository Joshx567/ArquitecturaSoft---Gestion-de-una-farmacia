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
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id);

                if (cliente == null)
                    return NotFound(new { message = "Cliente no encontrado" });

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClienteDto dto)
        {
            // Validar que el ID de la ruta coincida con el del DTO
            if (id != dto.id_cliente)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID del cliente" });
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var cliente = new Cliente
                {
                    id_cliente = dto.id_cliente,
                    tipo_cliente = dto.tipo_cliente,
                    nombre = dto.nombre,
                    ci = dto.ci,
                    edad = dto.edad,
                    sexo = dto.sexo,
                    telefono = dto.telefono
                };

                var resultado = await _clienteService.UpdateClienteAsync(cliente);

                if (resultado.success)
                {
                    return Ok(new { message = resultado.message });
                }
                else
                {
                    return BadRequest(new { message = resultado.message });
                }
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

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string? texto)
        {
            try
            {
                var clientes = await _clienteService.SearchClientesAsync(texto);
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