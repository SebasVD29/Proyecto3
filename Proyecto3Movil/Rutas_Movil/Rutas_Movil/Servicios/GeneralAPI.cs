using Rutas_Movil.IServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rutas_Movil.Servicios
{

    public class GeneralAPI : IGeneralAPI
    {

        //static readonly string direccionbase = "http://10.0.2.2:5000"; // Ip para el Emulador 
         static readonly string direccionbase = "http://10.18.9.26:5000"; //Ip de la Casa para el movil
        //static readonly string direccionbase = "http://10.18.7.173:5000";  //Ip de la U para el movil

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
