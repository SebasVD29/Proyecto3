namespace APIRutas_Movil.Modelo
{
    public class Rutas
    {
        public int IdentificadorRuta { get; set; }
        public string? Nombre { get; set; }
        public int? IdDireccionRuta { get; set; }
        public int? IdChofe { get; set; }
        public string? NumeroPlace { get; set; }
        public int? IdCliente { get; set; }
        public string? Descripcion { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFinal { get; set; }
        public string? EstadoEntrega { get; set; }
    }
}
