using Rutas_Movil.IServicios;
using Rutas_Movil.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using APIRutas_Movil.Modelo;

namespace Rutas_Movil.Servicios
{
    public class ServicioIncidente : IServicioIncidente
    {
        private readonly IGeneralAPI _generalAPI;

        public ServicioIncidente(IGeneralAPI generalAPI)
        {
            _generalAPI = generalAPI;
        }

        public async Task<Incidentes> CrearIncidencia(Incidentes Incidente)
        {
            var incidente = _generalAPI.GetHttpClient();

            var mensaje = new HttpRequestMessage(HttpMethod.Post, _generalAPI.URL("Incidente") + "CrearIncidente");
            mensaje.Content = JsonContent.Create<Incidentes>(Incidente);
            var response = await incidente.SendAsync(mensaje);
            response.EnsureSuccessStatusCode();

            var incidenteActualizado = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Incidentes>(incidenteActualizado);
        }
    }
}
