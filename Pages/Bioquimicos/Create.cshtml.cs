using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProyectoArqSoft.Modelos;
using ProyectoArqSoft.Service;

namespace ProyectoArqSoft.Pages.Bioquimicos
{
    public class CreateModel : PageModel
    {
        private readonly IBioquimicoService _service;

        public CreateModel(IBioquimicoService service)
        {
            _service = service;
        }

        [BindProperty]
        public Bioquimico Bioquimico { get; set; } = new();

        public string? MensajeError { get; set; }

        public void OnGet()
        {
            Bioquimico.activo = true;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var (ok, mensaje) = await _service.CrearAsync(Bioquimico);

            if (!ok)
            {
                MensajeError = mensaje;
                return Page();
            }

            return RedirectToPage("/Bioquimicos/Index");
        }
    }
}