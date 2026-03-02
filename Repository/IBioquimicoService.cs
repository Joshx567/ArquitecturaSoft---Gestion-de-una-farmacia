using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Service
{
    public interface IBioquimicoService
    {
        Task<(bool ok, string? mensaje)> CrearAsync(Bioquimico bioquimico);
        Task<List<Bioquimico>> ListarAsync();
    }
}