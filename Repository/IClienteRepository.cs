using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Repository
{
    public interface IClienteRepository
    {
        Task<int> CreateClienteAsync(Cliente cliente);
        Task<List<Cliente>> GetAllClientesAsync();
        Task<List<Cliente>> SearchClientesAsync(string texto);
    }
}