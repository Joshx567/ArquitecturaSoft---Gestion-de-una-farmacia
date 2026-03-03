using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Service
{
    public interface IClienteService
    {
        Task<int> CreateClienteAsync(Cliente cliente);
        Task<List<Cliente>> GetAllClientesAsync();
        Task<List<Cliente>> SearchClientesAsync(string? texto);
    }
}