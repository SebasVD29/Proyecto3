using Rutas_Movil.IServicios;

namespace Rutas_Movil.Vistas;

public partial class LogoutPage : ContentPage
{
    private readonly IServicioAutenticacion _servicioAutenticacion;
    private readonly IServicioRutas _servicioRutas;

    
    public LogoutPage(IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion)
	{
		InitializeComponent();
        _servicioRutas = servicioRutas;
        _servicioAutenticacion = servicioAutenticacion;
        SecureStorage.Remove("sesion");
        Navigation.PushAsync(new LoginPage(servicioAutenticacion, servicioRutas));
        
    }
}