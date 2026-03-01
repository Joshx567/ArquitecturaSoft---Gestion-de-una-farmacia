using ProyectoArqSoft.Model.Bioquimico;

namespace ProyectoArqSoft.Service
{
    public interface IBioquimicoService
    {
        
        Task<IEnumerable<Bioquimico>> BuscarAsync(string filtro);
    }
}