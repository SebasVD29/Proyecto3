using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Rutas_Movil.IServicios;
using Rutas_Movil.Modelos;
using Newtonsoft.Json;

namespace Rutas_Movil.Servicios
{
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly IGeneralAPI _generalAPI;

        public ServicioAutenticacion(IGeneralAPI generalAPI)
        {
            _generalAPI = generalAPI;
        }

       
        public async Task<ResponseChofer>  RealizarAutenticacionAsync(Chofer chofer)
            
        {
            var client = _generalAPI.GetHttpClient();
            var mensaje = new HttpRequestMessage(HttpMethod.Post, _generalAPI.URL("Choferes") +"Login");
     
                //var json = JsonConvert.SerializeObject(chofer);
                mensaje.Content = JsonContent.Create<Chofer>(chofer);
                var response = await client.SendAsync(mensaje);
                response.EnsureSuccessStatusCode();

                var resultadologin = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseChofer>(resultadologin);
            
                         
        }
    }
}
