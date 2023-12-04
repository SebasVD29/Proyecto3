namespace APIRutas_Movil.Modelo
{
    public class Incidente
    {
        public int IdentificadorIncidente { get; set; }
        public int IdRuta { get; set; }
        public string? Descripcion{ get; set; }
        public DateOnly? FechaHora { get; set; }
        public string? Solucion { get; set; }
   
    }
}
