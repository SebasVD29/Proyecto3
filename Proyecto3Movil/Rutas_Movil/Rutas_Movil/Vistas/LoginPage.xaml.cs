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
            // Recopila los datos del formulario u otros controles seg�n sea necesario
            string email = Email.Text;
            string contrase�a = Contrase�a.Text;

            // Crea una instancia de Chofer con los datos ingresados
            Chofer chofer = new Chofer();
            chofer.Email = email;
            chofer.Contrase�a = contrase�a;

            // Crea una instancia de tu servicio de autenticaci�n
            var login = await _servicioAutenticacion.RealizarAutenticacionAsync(chofer);

            if(login != null)
            {
                // Llamada al m�todo de autenticaci�n
                await RealizarAutenticacion(login);

            }else
            {
                // Muestra una alerta en caso de error
                await DisplayAlert("Ingreso fallido", "Usuario o contrase�a inv�lidos", "Pruebe de nuevo");
            }
        }

        private async Task RealizarAutenticacion(ResponseChofer responseChofer)
        {
            try
            {
                // Llamada al m�todo de autenticaci�n asincr�nico
                await SecureStorage.SetAsync("sesion", responseChofer.chofer.IdentificadorChofer.ToString());
                // Redirige a la p�gina de inicio despu�s de la autenticaci�n exitosa
                //await Shell.Current.GoToAsync("///home");
                await Navigation.PushAsync(new ListaRutasPage(responseChofer.chofer.IdentificadorChofer, _servicioRutas, _servicioAutenticacion));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante el inicio de sesi�n: {ex.Message}");


            }
        }
    }
}





//    Autenticacion login = new Autenticacion
//    {
//        IdentificadorChofer = usuario.Text,
//        Contrase�a = contrasena.Text
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