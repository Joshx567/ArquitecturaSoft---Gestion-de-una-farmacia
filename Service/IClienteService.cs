using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Service
{
    public interface IClienteService
    {
        Task<int> CreateClienteAsync(Cliente cliente);
        Task<List<Cliente>> GetAllClientesAsync();
        Task<List<Cliente>> SearchClientesAsync(string? texto);
        Task<Cliente?> GetClienteByIdAsync(int id);
        Task<(bool success, string message)> UpdateClienteAsync(Cliente cliente);
        bool ValidarEmail(string email);
        bool ValidarTelefono(string telefono);
    }
}