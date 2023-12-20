using Rutas_Movil.Modelos;
using Rutas_Movil.IServicios;

namespace Rutas_Movil.Vistas;

public partial class ListaRutasPage : ContentPage
{
    private readonly IServicioRutas _servicioRutas;
    private readonly IServicioAutenticacion _servicioAutenticacion;
    private readonly int _idChofer;
    public ListaRutasPage(int idChofer, IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion)
	{
		InitializeComponent();
        _servicioRutas = servicioRutas;
        _servicioAutenticacion = servicioAutenticacion; 
        _idChofer = idChofer;
        cargaRutas(_idChofer);
    }
    async void cargaRutas(int idChofer)
    {
        carga.IsVisible = true;

        var listaRutas = await _servicioRutas.ListaRutas(idChofer);
        lvRutas.ItemsSource = listaRutas;
        carga.IsVisible = false;
    }

    private async void Crear_Clicked(object sender, EventArgs e)
    {

        //await Navigation.PushAsync(new CrearIncidentePage(_servicioRutas));
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Rutas item = args.SelectedItem as Rutas;

        Navigation.PushAsync(new DetallesRutaPage(item, _idChofer, _servicioRutas, _servicioAutenticacion));
    }
    private async void LogOut_Clicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new LogoutPage(_servicioRutas, _servicioAutenticacion));
    }
}