using System.Data;

namespace APIRutas_Movil.IDapper
{
    public interface IDapperContext
    {
        public IDbConnection CrearConexion();
    }
}
