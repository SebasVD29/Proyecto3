using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rutas_Movil.IServicios
{
    public interface IGeneralAPI
    {
        public HttpClient GetHttpClient();
        public string URL(string controller);
    }
}
