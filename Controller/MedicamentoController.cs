using Microsoft.AspNetCore.Mvc;
using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Modelos.Dto;
using ProyectoArqSoft.Service;

namespace ProyectoArqSoft.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentoController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMedicamentoService _service;

        public MedicamentoController(IMedicamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMedicamentoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            var new_medicamento = new Medicamento
            {
                nombre = dto.nombre,
                presentacion = dto.presentacion,
                clasificacion = dto.clasificacion,
                concentracion = dto.concentracion,
                stock_minimo = dto.stock_minimo
            };

            try
            {
                int newId = await _service.CrearAsync(new_medicamento);
                return Ok(new { id = newId, message = "Creado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}