using APIRutas_Movil.Modelo;

namespace APIRutas_Movil.IRepositorySQL
{
    public interface IChoferRepository
    {
            public Task<Chofer> LoginChofer(string password, string email);
    }
}
