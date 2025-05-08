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

            var rutaActualizado = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponseRutas>(rutaActualizado);


        }

        public async Task<List<Rutas>> ListaRutas(int IdChofer)
        {
            try
            {

                var rutas = _generalAPI.GetHttpClient();
               // Uri uri = new Uri(string.Format(_generalAPI.URL("Rutas") + "ListaRutasPorChofer/", IdChofer));
                // param = string.Format("ListaRutasPorChofer/IdChofer={0}", IdChofer);

                string result = await rutas.GetStringAsync(_generalAPI.URL("Rutas") + "ListaRutasPorChofer/"+ IdChofer);

                var resultado = JsonConvert.DeserializeObject<ResponseListaRutas>(result);

                var lista = resultado.ruta.ToList();

                return lista;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
