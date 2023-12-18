
using Rutas_Movil.Modelos;

namespace APIRutas_Movil.Modelo
{
    public class ResponseIncidente
    {
        public Incidentes Incidentes { get; set; } = new Incidentes();
        public ResponseModel Errores { get; set; } = new ResponseModel();


    }
}
