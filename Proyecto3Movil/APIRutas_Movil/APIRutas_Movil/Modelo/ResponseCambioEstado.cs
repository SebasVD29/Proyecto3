namespace APIRutas_Movil.Modelo
{
    public class ResponseCambioEstado
    {
        public Rutas Ruta { get; set; } = new Rutas();
        public ResponseModel errores { get; set; } = new ResponseModel();
    }
}
