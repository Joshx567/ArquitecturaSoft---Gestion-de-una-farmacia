using System.ComponentModel.DataAnnotations;

namespace ProyectoArqSoft.Modelos.Dto
{
    public class CreateBioquimicoDto
    {
        [Required] public string nombres { get; set; } = string.Empty;
        [Required] public string apellidos { get; set; } = string.Empty;
        [Required] public string ci { get; set; } = string.Empty;
        [Required] public string telefono { get; set; } = string.Empty;
        public bool activo { get; set; } = true;
    }
}