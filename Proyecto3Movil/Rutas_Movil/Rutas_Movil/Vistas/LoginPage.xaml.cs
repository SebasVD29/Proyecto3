using Rutas_Movil.IServicios;
using Rutas_Movil.Modelos;
using Rutas_Movil.Servicios;
using Rutas_Movil.Vistas;

namespace Rutas_Movil.Vistas
{
    public partial class LoginPage : ContentPage
    {
        private readonly IServicioRutas _servicioRutas;
        private readonly IServicioAutenticacion _servicioAutenticacion;
        private readonly IServicioIncidente _servicioIncidente;


        public LoginPage(IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion, IServicioIncidente servicioIncidente)
        {
            InitializeComponent();
            _servicioRutas = servicioRutas;
            _servicioAutenticacion = servicioAutenticacion;
            _servicioIncidente = servicioIncidente;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            // Recopila los datos del formulario u otros controles seg�n sea necesario
            string email = Email.Text;
            string contrase�a = Contrase�a.Text;

            // Crea una instancia de Chofer con los datos ingresados
            Chofer chofer = new Chofer();
            chofer.Email = email;
            chofer.Contrase�a = contrase�a;

            // Crea una instancia de tu servicio de autenticaci�n
            var login = await _servicioAutenticacion.RealizarAutenticacionAsync(chofer);
            await RealizarAutenticacion(login);

        }

        private async Task RealizarAutenticacion(ResponseChofer responseChofer)
        {
            if (responseChofer.errores.errorcode == 0)
            {
            
                // Llamada al m�todo de autenticaci�n asincr�nico
                await SecureStorage.SetAsync("sesion", responseChofer.chofer.IdentificadorChofer.ToString());
                // Redirige a la p�gina de inicio despu�s de la autenticaci�n exitosa
                await Navigation.PushAsync(new ListaRutasPage(responseChofer.chofer.IdentificadorChofer, _servicioRutas, _servicioAutenticacion, _servicioIncidente));

            }
            else
            {
                // Muestra una alerta en caso de error
                await DisplayAlert("Ingreso fallido", "Usuario o contrase�a inv�lidos", "Pruebe de nuevo");
            }
        }
    }
}




