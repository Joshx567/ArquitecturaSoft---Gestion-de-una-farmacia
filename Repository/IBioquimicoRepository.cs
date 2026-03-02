using ProyectoArqSoft.Modelos;

namespace ProyectoArqSoft.Repository
{
    public interface IBioquimicoRepository
    {
        Task<int> CrearAsync(Bioquimico bioquimico);
        Task<List<Bioquimico>> ListarAsync();

        Task<bool> ExisteCiAsync(string ci);
    }
}