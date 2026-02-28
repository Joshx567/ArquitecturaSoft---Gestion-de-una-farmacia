using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Service; 
public interface IMedicamentoService
{
    Task<int> CrearAsync(Medicamento medicamento);
}