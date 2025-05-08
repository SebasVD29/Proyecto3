namespace APIRutas_Movil.Modelo
{
    public class ResponseListaRutas
    {
        public List<Rutas> ruta { get; set; } = new List<Rutas>();
        public ResponseModel errores { get; set; } = new ResponseModel();
    }
}
