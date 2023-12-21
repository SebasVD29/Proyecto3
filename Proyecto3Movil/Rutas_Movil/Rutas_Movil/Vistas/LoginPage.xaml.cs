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
            // Recopila los datos del formulario u otros controles según sea necesario
            string email = Email.Text;
            string contraseña = Contraseña.Text;

            // Crea una instancia de Chofer con los datos ingresados
            Chofer chofer = new Chofer();
            chofer.Email = email;
            chofer.Contraseña = contraseña;

            // Crea una instancia de tu servicio de autenticación
            var login = await _servicioAutenticacion.RealizarAutenticacionAsync(chofer);
            await RealizarAutenticacion(login);

        }

        private async Task RealizarAutenticacion(ResponseChofer responseChofer)
        {
            if (responseChofer.errores.errorcode == 0)
            {
            
                // Llamada al método de autenticación asincrónico
                await SecureStorage.SetAsync("sesion", responseChofer.chofer.IdentificadorChofer.ToString());
                // Redirige a la página de inicio después de la autenticación exitosa
                await Navigation.PushAsync(new ListaRutasPage(responseChofer.chofer.IdentificadorChofer, _servicioRutas, _servicioAutenticacion, _servicioIncidente));

            }
            else
            {
                // Muestra una alerta en caso de error
                await DisplayAlert("Ingreso fallido", "Usuario o contraseña inválidos", "Pruebe de nuevo");
            }
        }
    }
}




