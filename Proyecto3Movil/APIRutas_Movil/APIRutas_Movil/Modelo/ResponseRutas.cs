namespace APIRutas_Movil.Modelo
{
    public class ResponseRutas
    {
        public Rutas ruta { get; set; } = new Rutas();
        public ResponseModel errores { get; set; } = new ResponseModel();
    }
}
