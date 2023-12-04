
namespace APIRutas_Movil.Modelo
{
    public class ResponseIncidente
    {
        public Incidente Incidente { get; set; } = new Incidente();
        public ResponseModel Errores { get; set; } = new ResponseModel();
    }
}
