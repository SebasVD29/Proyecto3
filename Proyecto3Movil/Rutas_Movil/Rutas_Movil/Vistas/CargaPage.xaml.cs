using Rutas_Movil.IServicios;

namespace Rutas_Movil.Vistas;

public partial class CargaPage : ContentPage
{
    private readonly IServicioRutas _servicioRutas;
    private readonly IServicioAutenticacion _servicioAutenticacion;
    public CargaPage(IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion)
	{
		InitializeComponent();
        
        _servicioAutenticacion = servicioAutenticacion;
        _servicioRutas = servicioRutas;
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (await estaAutenticado() != null)
        {
            int idChofer = int.Parse(await estaAutenticado());


            await Navigation.PushAsync(new ListaRutasPage(idChofer, _servicioRutas, _servicioAutenticacion));
            //await Shell.Current.GoToAsync($"///home/rutas?idChofer={idChofer}&servicioRutas={_servicioRutas}");

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