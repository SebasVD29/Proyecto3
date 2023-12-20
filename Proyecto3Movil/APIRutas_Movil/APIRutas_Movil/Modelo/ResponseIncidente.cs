
namespace APIRutas_Movil.Modelo
{
    public class ResponseIncidente
    {
        public Incidente incidente { get; set; } = new Incidente();
        public ResponseModel errores { get; set; } = new ResponseModel();
    }
}
