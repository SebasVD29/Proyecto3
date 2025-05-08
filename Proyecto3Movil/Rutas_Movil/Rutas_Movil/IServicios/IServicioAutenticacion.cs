using Rutas_Movil.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Rutas_Movil.IServicios
{
    public interface IServicioAutenticacion
    {
        Task<ResponseChofer> RealizarAutenticacionAsync(Chofer chofer);
    }
}
