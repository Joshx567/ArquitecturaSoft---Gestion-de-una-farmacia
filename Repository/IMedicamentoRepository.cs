using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Repository; 

public interface IMedicamentoRepository
{
    Task<int> CrearAsync(Medicamento medicamento);
    Task<List<Medicamento>> ListarAsync();  
}