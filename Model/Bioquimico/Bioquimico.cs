namespace ProyectoArqSoft.Model.Bioquimico
{
    public class Bioquimico
    {
        public int id_bioquimico { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string ci { get; set; }
        public string telefono { get; set; }
        public bool activo { get; set; }
    }
}