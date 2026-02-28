using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Repository;

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

}
    
