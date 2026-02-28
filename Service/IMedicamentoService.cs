using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Models.Dto;

namespace ProyectoArqSoft.Service; 
public interface IMedicamentoService
{
    Task<int> CrearAsync(Medicamento medicamento);
    Task<List<ListMedicamentoDto>> ListarAsync();
    
}