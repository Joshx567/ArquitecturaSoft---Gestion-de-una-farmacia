using System.Text.RegularExpressions;
using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Repository;

namespace ProyectoArqSoft.Service
{
    public class BioquimicoService : IBioquimicoService
    {
        private readonly IBioquimicoRepository _repo;

        public BioquimicoService(IBioquimicoRepository repo)
        {
            _repo = repo;
        }

        public async Task<(bool ok, string? mensaje)> CrearAsync(Bioquimico b)
        {
            if (string.IsNullOrWhiteSpace(b.nombres) ||
                string.IsNullOrWhiteSpace(b.apellidos) ||
                string.IsNullOrWhiteSpace(b.ci) ||
                string.IsNullOrWhiteSpace(b.telefono))
            {
                return (false, "Complete los campos obligatorios");
            }

            var tel = b.telefono.Trim();
            if (!Regex.IsMatch(tel, @"^\d{7,10}$"))
            {
                return (false, "Teléfono inválido");
            }

            var ci = b.ci.Trim();
            if (await _repo.ExisteCiAsync(ci))
            {
                return (false, "Bioquímico ya registrado");
            }

            b.nombres = b.nombres.Trim();
            b.apellidos = b.apellidos.Trim();
            b.ci = ci;
            b.telefono = tel;

            await _repo.CrearAsync(b);
            return (true, null);
        }

        public Task<List<Bioquimico>> ListarAsync()
        {
            return _repo.ListarAsync();
        }
    }
}