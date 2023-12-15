using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rutas_Movil.Modelos
{
    public class ResponseRutas
    {
        public Rutas ruta { get; set; } = new Rutas();
        public ResponseModel errores { get; set; } = new ResponseModel();
    }
}
