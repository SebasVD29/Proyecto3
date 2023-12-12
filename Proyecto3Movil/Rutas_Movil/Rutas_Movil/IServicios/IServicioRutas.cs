using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rutas_Movil.Modelos;

namespace Rutas_Movil.IServicios
{
    public interface IServicioRutas
    {
        public Task<List<Rutas>> ListaRutas(int idChofer);
        public Task<ResponseRutas> ActualizarEstadoRuta(Rutas ruta);
    }
}
