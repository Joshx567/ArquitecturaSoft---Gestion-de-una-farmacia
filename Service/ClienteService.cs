using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Repository;

namespace ProyectoArqSoft.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<int> CreateClienteAsync(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.tipo_cliente))
                throw new ArgumentException("El tipo de cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.ci))
                throw new ArgumentException("El CI es obligatorio.");

            return await _clienteRepository.CreateClienteAsync(cliente);
        }

        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _clienteRepository.GetAllClientesAsync();
        }

        public async Task<List<Cliente>> SearchClientesAsync(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await _clienteRepository.GetAllClientesAsync();

            texto = texto.Trim();

            bool pareceNumero = texto.All(char.IsDigit);
            bool contieneLetras = texto.Any(char.IsLetter);

            if (pareceNumero && texto.Length < 5)
                throw new ArgumentException("Criterio inválido");

            if (!pareceNumero && !contieneLetras)
                throw new ArgumentException("Criterio inválido");

            return await _clienteRepository.SearchClientesAsync(texto);
        }
    }
}