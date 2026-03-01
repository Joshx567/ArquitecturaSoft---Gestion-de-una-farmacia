using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Repository;
using System.Text.RegularExpressions;

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
            // Validar formato de teléfono
            if (!ValidarTelefono(cliente.telefono))
                throw new ArgumentException("El formato del teléfono no es válido. Debe tener 7-8 dígitos");

            // Validar que el CI no exista
            if (await _clienteRepository.ExisteCiAsync(cliente.ci))
                throw new ArgumentException("Ya existe un cliente con ese CI");

            return await _clienteRepository.CreateClienteAsync(cliente);
        }

        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _clienteRepository.GetAllClientesAsync();
        }

        public async Task<List<Cliente>> SearchClientesAsync(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return await GetAllClientesAsync();

            return await _clienteRepository.SearchClientesAsync(texto);
        }

        public async Task<Cliente?> GetClienteByIdAsync(int id)
        {
            return await _clienteRepository.GetClienteByIdAsync(id);
        }

        public async Task<(bool success, string message)> UpdateClienteAsync(Cliente cliente)
        {
            try
            {
                // Validar que el cliente existe
                var clienteExistente = await _clienteRepository.GetClienteByIdAsync(cliente.id_cliente);
                if (clienteExistente == null)
                {
                    return (false, "Cliente no encontrado");
                }

                // Validar formato de teléfono
                if (!ValidarTelefono(cliente.telefono))
                {
                    return (false, "El formato del teléfono no es válido. Debe tener 7-8 dígitos");
                }

                // Validar edad si está presente
                if (cliente.edad.HasValue && (cliente.edad < 0 || cliente.edad > 120))
                {
                    return (false, "La edad debe estar entre 0 y 120 años");
                }

                // Validar sexo si está presente
                if (!string.IsNullOrEmpty(cliente.sexo) && cliente.sexo != "M" && cliente.sexo != "F")
                {
                    return (false, "El sexo debe ser M o F");
                }

                // Validar que el CI no exista en otro cliente
                if (await _clienteRepository.ExisteCiAsync(cliente.ci, cliente.id_cliente))
                {
                    return (false, "Ya existe un cliente con ese CI");
                }

                // Actualizar cliente
                var actualizado = await _clienteRepository.UpdateClienteAsync(cliente);

                if (actualizado)
                {
                    return (true, "Cliente actualizado exitosamente");
                }
                else
                {
                    return (false, "No se pudo actualizar el cliente");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error al actualizar: {ex.Message}");
            }
        }

        public bool ValidarTelefono(string telefono)
        {
            if (string.IsNullOrEmpty(telefono))
                return false;

            // Validar formato de teléfono boliviano
            // Acepta: 12345678, 1234567, +59112345678, +591 12345678, 59112345678
            var regexTelefono = new Regex(@"^(\+?591)?[ -]?[0-9]{7,8}$");
            return regexTelefono.IsMatch(telefono.Trim());
        }

        public bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return true; 

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }

}