using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rutas_Movil.Modelos
{
    public class Incidentes 
    {
        public int IdentificadorIncidente { get; set; }
        public int IdRuta { get; set; }
        public string? Descripcion { get; set; }
        public string? FechaHora { get; set; }
        public string? Solucion { get; set; }

    }
}
