using Rutas_Movil.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rutas_Movil.Servicios
{
    //201.203.6.42
    public class GeneralAPI : IGeneralAPI
    {
        //static readonly string direccionbase = "http://localhost:7059";
        //static readonly string direccionbase = "http://10.18.9.23";
        //static readonly string direccionbase = "https://192.168.100.8:5000";
        static readonly string direccionbase = "http://192.168.0.9";

        static readonly string _url = $"{direccionbase}/api/";

        public HttpClient GetHttpClient()
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;

        }

        public string URL(string controller)
        {
            return _url + controller + "/";
        }
    }
}
