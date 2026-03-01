namespace ProyectoArqSoft.Modelos
{
    public class Cliente
    {
        public int id_cliente { get; set; }
        public string tipo_cliente { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string ci { get; set; }  = string.Empty;
        public int? edad { get; set; }
        public string sexo { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
    }
}