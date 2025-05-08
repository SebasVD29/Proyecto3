

namespace Rutas_Movil.Modelos
{
    public class ResponseIncidente
    {
        public Incidente incidente { get; set; } = new Incidente();
        public ResponseModel errores { get; set; } = new ResponseModel();


    }
}
