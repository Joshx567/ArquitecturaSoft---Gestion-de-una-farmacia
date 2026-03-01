using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Repository;
using ProyectoArqSoft.Models.Dto;

namespace ProyectoArqSoft.Service; 
public class MedicamentoService: IMedicamentoService
{
    private readonly IMedicamentoRepository _repo; 
    public MedicamentoService(IMedicamentoRepository repo) => _repo = repo; 
    public async Task<int> CrearAsync(Medicamento medicamento)
    {
        if (medicamento == null) throw new ArgumentNullException(nameof(medicamento));

        if (string.IsNullOrWhiteSpace(medicamento.nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(medicamento.presentacion))
            throw new ArgumentException("La presentación es obligatoria.");

        if (string.IsNullOrWhiteSpace(medicamento.clasificacion))
            throw new ArgumentException("La clasificación es obligatoria.");
        if(string.IsNullOrWhiteSpace(medicamento.concentracion))
            throw new ArgumentException("La concentracion es obligatoria.");

        if (medicamento.stock_minimo < 0)
            throw new ArgumentException("El stock mínimo no puede ser negativo.");

        return await _repo.CrearAsync(medicamento);
    }

    public async Task<List<ListMedicamentoDto>> ListarAsync()
    {
        var lista = await _repo.ListarAsync();

        return lista.Select(m => new ListMedicamentoDto
        {
            id_medicamento = m.id_medicamento,
            nombre = m.nombre,
            presentacion = m.presentacion,
            clasificacion = m.clasificacion,
            stock_minimo = m.stock_minimo
        }).ToList();
    }

    public async Task<bool> ActualizarAsync(UpdateMedicamentoDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (dto.id_medicamento <= 0) throw new ArgumentException("ID inválido.");

        if (string.IsNullOrWhiteSpace(dto.nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(dto.presentacion))
            throw new ArgumentException("La presentación es obligatoria.");

        if (string.IsNullOrWhiteSpace(dto.clasificacion))
            throw new ArgumentException("La clasificación es obligatoria.");

        if (dto.stock_minimo < 0)
            throw new ArgumentException("El stock mínimo no puede ser negativo.");

        var medicamento = new Medicamento
        {
            id_medicamento = dto.id_medicamento,
            nombre = dto.nombre,
            presentacion = dto.presentacion,
            clasificacion = dto.clasificacion,
            concentracion = dto.concentracion, // quita si no existe
            stock_minimo = dto.stock_minimo
        };

        return await _repo.ActualizarAsync(medicamento);
    }

}
    
