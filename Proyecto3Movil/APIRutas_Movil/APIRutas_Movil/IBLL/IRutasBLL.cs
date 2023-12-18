using APIRutas_Movil.Modelo;

namespace APIRutas_Movil.IBLL
{
    public interface IRutasBLL
    {
        public Task<ResponseRutas> ListarRutasPorChofer(int idChofer);
        public Task<Boolean> CambioEstado(Rutas rutas);


    }
}
