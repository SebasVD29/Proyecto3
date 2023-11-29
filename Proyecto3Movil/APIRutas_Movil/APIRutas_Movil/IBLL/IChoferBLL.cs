using APIRutas_Movil.Modelo;

namespace APIRutas_Movil.IBLL
{
    public interface IChoferBLL
    {
        public Task<ResponseChofer> LoginChofer(string password, string email);
        public Task<string> Encriptacion(string password);

    }
}
