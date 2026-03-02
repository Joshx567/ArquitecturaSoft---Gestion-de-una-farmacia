namespace ProyectoArqSoft.Modelos
{
    public class Bioquimico
    {
        public int id_bioquimico { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;
        public string ci { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public bool activo { get; set; } = true;
    }
}