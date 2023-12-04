namespace APIRutas_Movil.Modelo
{
    public class Rutas
    {
        public int IdentificadorRuta { get; set; }
        public string? Nombre { get; set; }
        public int? IdDireccionRuta { get; set; }
        public int? IdChofer { get; set; }
        public string? NumeroPlaca { get; set; }
        public int? IdCliente { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public string? EstadoEntrega { get; set; }
    }


}
