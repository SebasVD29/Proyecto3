namespace APIRutas_Movil.Modelo
{
    public class ResponseChofer
    {
        public Chofer chofer { get; set; } = new Chofer();
        public ResponseModel errores { get; set; } = new ResponseModel();
    }
}
