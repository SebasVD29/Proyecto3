using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rutas_Movil.Modelos
{
    public class ResponseListaRutas
    {
        public List<Rutas> ruta { get; set; } = new List<Rutas>();
        public ResponseModel errores { get; set; } = new ResponseModel();
    }
}
