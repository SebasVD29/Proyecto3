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
    public class ServicioRutas : IServicioRutas
    {
        private readonly IGeneralAPI _generalAPI;

        public ServicioRutas(IGeneralAPI generalAPI)
        {
            _generalAPI = generalAPI;
        }

        public async Task<ResponseRutas> ActualizarEstadoRuta(Rutas rutas)
        {
            var ruta = _generalAPI.GetHttpClient();

            var mensaje = new HttpRequestMessage(HttpMethod.Post, _generalAPI.URL("Rutas") + "ActualizarEstado");
            mensaje.Content = JsonContent.Create<Rutas>(rutas);
            var response = await ruta.SendAsync(mensaje);
            response.EnsureSuccessStatusCode();

            var clienteActualizado = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseRutas>(clienteActualizado);


        }

        public async Task<List<Rutas>> ListaRutas(int IdChofer)
        {
            IdChofer = 258741369;
            var rutas = _generalAPI.GetHttpClient();

            string result = await rutas.GetStringAsync(_generalAPI.URL("Rutas") + "ListaRutasPorChofer?IdChofer=" + IdChofer);

            var resultado = JsonConvert.DeserializeObject<ResponseListaRutas>(result);

            var lista = resultado.rutas.ToList();

            return lista;
        }
    }
}
