namespace APIRutas_Movil.Modelo
{
    public class Rutas
    {
        public int IdentificadorRuta { get; set; }
        public string? NombreRuta { get; set; }

        public string? NombreDireccionRuta { get; set; }
        public string? PaisFinal { get; set; }
        public string? CiudadFinal { get; set; }

        public int? IdChofer { get; set; }
        public string? NumeroPlaca { get; set; }
        public int? IdCliente { get; set; }

        public string? NombreCliente { get; set; }
        public string? TelefonoCliente { get; set; }

        public string? Descripcion { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFinal { get; set; }
        public string? EstadoEntrega { get; set; }
    }
}
