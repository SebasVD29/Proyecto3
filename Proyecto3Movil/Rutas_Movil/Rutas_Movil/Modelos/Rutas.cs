using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rutas_Movil.Modelos
{
    public class Rutas
    {
        public int IdentificadorRuta { get; set; }
        public string? Nombre { get; set; }
        public int? IdDireccionRuta { get; set; }
        public int? IdChofer { get; set; }
        public string? NumeroPlaca { get; set; }
        public int? IdCliente { get; set; }
        public string? Descripcion { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFinal { get; set; }
        public string? EstadoEntrega { get; set; }
    }
}
