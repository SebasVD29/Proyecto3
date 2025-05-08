namespace APIRutas_Movil.Modelo
{
    public class Incidente
    {
        public int IdentificadorIncidente { get; set; }
        public int IdRuta { get; set; }
        public string? Descripcion { get; set; }
        public string? FechaHora { get; set; }
        public string? Solucion { get; set; }
   
    }
}
