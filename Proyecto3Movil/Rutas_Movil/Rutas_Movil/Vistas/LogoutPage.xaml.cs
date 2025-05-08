using Rutas_Movil.IServicios;
using Rutas_Movil.Modelos;

namespace Rutas_Movil.Vistas;

public partial class LogoutPage : ContentPage
{
    private readonly IServicioRutas _servicioRutas;
    private readonly IServicioAutenticacion _servicioAutenticacion;
    private readonly IServicioIncidente _servicioIncidente;
    


    public LogoutPage(IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion, IServicioIncidente servicioIncidente)
	{
		InitializeComponent();
        _servicioRutas = servicioRutas;
        _servicioAutenticacion = servicioAutenticacion;
        _servicioIncidente = servicioIncidente;
        SecureStorage.Remove("sesion");
        Navigation.PushAsync(new LoginPage(_servicioRutas, _servicioAutenticacion, _servicioIncidente));
        
    }
}