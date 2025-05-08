
using Rutas_Movil.IServicios;
using Rutas_Movil.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace Rutas_Movil.Servicios
{
    public class ServicioIncidente : IServicioIncidente
    {
        private readonly IGeneralAPI _generalAPI;

        public ServicioIncidente(IGeneralAPI generalAPI)
        {
            _generalAPI = generalAPI;
        }

        public async Task<ResponseIncidente> CrearIncidencia(Incidente incidente)
        {
            var client = _generalAPI.GetHttpClient();

            var mensaje = new HttpRequestMessage(HttpMethod.Post, _generalAPI.URL("Incidente") + "CrearIncidencia");
            mensaje.Content = JsonContent.Create<Incidente>(incidente);

            var response = await client.SendAsync(mensaje);
            response.EnsureSuccessStatusCode();

            var incidenteCreado = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseIncidente>(incidenteCreado);
        }
    }
}
