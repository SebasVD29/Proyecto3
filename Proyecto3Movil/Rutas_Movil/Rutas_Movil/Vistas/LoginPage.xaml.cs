using Rutas_Movil.IServicios;
using Rutas_Movil.Modelos;
using Rutas_Movil.Servicios;
using Rutas_Movil.Vistas;

namespace Rutas_Movil.Vistas
{
    public partial class LoginPage : ContentPage
    {   
        private readonly IServicioAutenticacion _servicioAutenticacion;
        private readonly IServicioRutas _servicioRutas;

        public LoginPage(IServicioAutenticacion servicioAutenticacion, IServicioRutas servicioRutas)
        {
            InitializeComponent();
            _servicioAutenticacion = servicioAutenticacion;
            _servicioRutas = servicioRutas;
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

            if(login != null)
            {
                // Llamada al método de autenticación
                await RealizarAutenticacion(login);

            }else
            {
                // Muestra una alerta en caso de error
                await DisplayAlert("Ingreso fallido", "Usuario o contraseña inválidos", "Pruebe de nuevo");
            }
        }

        private async Task RealizarAutenticacion(ResponseChofer responseChofer)
        {
            try
            {
                // Llamada al método de autenticación asincrónico
                await SecureStorage.SetAsync("sesion", responseChofer.chofer.IdentificadorChofer.ToString());
                // Redirige a la página de inicio después de la autenticación exitosa
                //await Shell.Current.GoToAsync("///home");
                await Navigation.PushAsync(new ListaRutasPage(responseChofer.chofer.IdentificadorChofer, _servicioRutas, _servicioAutenticacion));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante el inicio de sesión: {ex.Message}");


            }
        }
    }
}





//    Autenticacion login = new Autenticacion
//    {
//        IdentificadorChofer = usuario.Text,
//        Contraseña = contrasena.Text
//    };

//    Uri RequestUri =new Uri("");
//     var client = new HttpClient();
//    var json= JsonConvert.SerializeObject(login);
//    var contentJson= new StringContent(json,Encoding.UTF8,"application/json");
//    var response =await client.PostAsync(RequestUri, contentJson);
//    if (response.StatusCode == HttpStatusCode.OK)

//    {

//        await Navigation.PushAsync(new HomePage());

//    }
//    else {


//        DisplayAlert("Mensaje", "Datos invalidos", "ok ");
//    }