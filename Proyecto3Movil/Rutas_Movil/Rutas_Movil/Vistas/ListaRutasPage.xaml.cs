using Rutas_Movil.Modelos;
using Rutas_Movil.IServicios;

namespace Rutas_Movil.Vistas;

public partial class ListaRutasPage : ContentPage
{
    private readonly IServicioRutas _servicioRutas;
    private readonly IServicioAutenticacion _servicioAutenticacion;
    private readonly IServicioIncidente _servicioIncidente;
    

    private readonly int _idChofer;
    public ListaRutasPage(int idChofer, IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion, IServicioIncidente servicioIncidente)
	{
		InitializeComponent();
        _servicioRutas = servicioRutas;
        _servicioAutenticacion = servicioAutenticacion;
        _servicioIncidente = servicioIncidente;
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

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        Rutas item = args.SelectedItem as Rutas;

        Navigation.PushAsync(new DetallesRutaPage(item, _idChofer, _servicioRutas, _servicioAutenticacion, _servicioIncidente));
    }
    private async void LogOut_Clicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new LogoutPage(_servicioRutas, _servicioAutenticacion, _servicioIncidente));
    }
}