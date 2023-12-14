using Rutas_Movil.Modelos;
using Rutas_Movil.IServicios;

namespace Rutas_Movil.Vistas;

public partial class ListaRutasPage : ContentPage
{
    private readonly IServicioRutas _servicioRutas;
    int idChofer = 258741369;
    public ListaRutasPage(IServicioRutas servicioRutas)
	{
		InitializeComponent();
        _servicioRutas = servicioRutas;
        cargaRutas();
    }
    async void cargaRutas()
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

        Navigation.PushAsync(new DetallesRutaPage(item, _servicioRutas));
    }
}