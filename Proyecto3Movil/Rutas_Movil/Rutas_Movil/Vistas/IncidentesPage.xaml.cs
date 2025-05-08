namespace Rutas_Movil.Vistas;
using Rutas_Movil.IServicios;
using Rutas_Movil.Modelos;

public partial class IncidentesPage : ContentPage
{

    private readonly IServicioRutas _servicioRutas;
    private readonly IServicioAutenticacion _servicioAutenticacion;
    private readonly IServicioIncidente _servicioIncidente;
    private readonly int _idChofer;
    private readonly Rutas _rutas;

    public IncidentesPage(Rutas rutas, int idChofer, IServicioIncidente servicioIncidentes, IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion)
    {
        InitializeComponent();
        _servicioIncidente = servicioIncidentes;
        _servicioRutas = servicioRutas;
        _servicioAutenticacion = servicioAutenticacion;
        _idChofer = idChofer;
        _rutas = rutas;
        Carga_Datos(_rutas);
    }

    void Carga_Datos(Rutas rutas)
    {
        idRuta.Text = rutas.IdentificadorRuta.ToString();
        
    }

    private async void CrearIncidencia()
    {
 
        Incidente nuevoIncidente = new Incidente()
        {
            IdRuta = _rutas.IdentificadorRuta,
            Descripcion = descripcion.Text,
            FechaHora = fecha.Date.ToString("yyyy-MM-dd"),
            Solucion = solucion.Text
        };

        var responseIncidente = await _servicioIncidente.CrearIncidencia(nuevoIncidente);

        if (responseIncidente.errores.errorcode == 0)
        {
            await DisplayAlert("Creación Exitosa", "El incidente ha sido creado con éxito", "OK");
            await Task.Delay(1000);
            await Navigation.PushAsync(new DetallesRutaPage(_rutas, _idChofer, _servicioRutas, _servicioAutenticacion, _servicioIncidente));
        }
        else
        {
            Mensaje.IsVisible = true;
            Mensaje.Text = "Error al crear el cliente";
        }
    }
    private void Crear_Clicked(object sender, EventArgs e)
    {
        CrearIncidencia();
    }

    async void Regresar()
    {
        
        await Navigation.PushAsync(new DetallesRutaPage(_rutas, _idChofer, _servicioRutas, _servicioAutenticacion, _servicioIncidente));
    }
    private void Volver_Clicked(object sender, EventArgs e)
    {
        Regresar();
    }
}

