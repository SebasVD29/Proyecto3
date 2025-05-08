using Rutas_Movil.IServicios;
using Rutas_Movil.Modelos;

namespace Rutas_Movil.Vistas;

public partial class CargaPage : ContentPage
{
    private readonly IServicioRutas _servicioRutas;
    private readonly IServicioAutenticacion _servicioAutenticacion;
    private readonly IServicioIncidente _servicioIncidente;
    
    public CargaPage(IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion, IServicioIncidente servicioIncidente)

    {
		InitializeComponent();
        _servicioRutas = servicioRutas;
        _servicioAutenticacion = servicioAutenticacion;
        _servicioIncidente = servicioIncidente;
    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (await estaAutenticado() != null)
        {
            int idChofer = int.Parse(await estaAutenticado());


            await Navigation.PushAsync(new ListaRutasPage(idChofer, _servicioRutas, _servicioAutenticacion, _servicioIncidente));
         

        }
        else
        {
            await Shell.Current.GoToAsync("login");
        }
        base.OnNavigatedTo(args);
    }

    async Task<string> estaAutenticado()
    {
        await Task.Delay(2000);
        var idChofer = await SecureStorage.GetAsync("sesion");
        return idChofer;



    }
}