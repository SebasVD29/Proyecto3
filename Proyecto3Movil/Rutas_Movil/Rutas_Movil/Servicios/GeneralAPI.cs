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
        static readonly string direccionbase = "http://localhost:7059";
        //static readonly string direccionbase = "http://10.0.2.2:5000";
        //static readonly string direccionbase = "https://192.168.100.8:5000";
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
