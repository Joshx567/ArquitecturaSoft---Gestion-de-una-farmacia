using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Service;

namespace ProyectoArqSoft.Pages.Bioquimicos
{
    public class IndexModel : PageModel
    {
        private readonly IBioquimicoService _service;

        public IndexModel(IBioquimicoService service)
        {
            _service = service;
        }

        public List<Bioquimico> Bioquimicos { get; private set; } = new();

        public async Task OnGetAsync()
        {
            Bioquimicos = await _service.ListarAsync();
        }
    }
}