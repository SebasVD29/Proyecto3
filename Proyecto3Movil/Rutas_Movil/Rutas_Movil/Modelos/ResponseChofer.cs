
namespace Rutas_Movil.Modelos
{


    public class ResponseChofer
{
    public Chofer chofer { get; set; } = new Chofer();
    public ResponseModel errores { get; set; } = new ResponseModel();
}
}