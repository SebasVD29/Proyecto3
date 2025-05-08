using Rutas_Movil.Modelos;
using Rutas_Movil.IServicios;

namespace Rutas_Movil.Vistas;

public partial class DetallesRutaPage : ContentPage
{
    private int idRuta = 0;
    
    private readonly IServicioRutas _servicioRutas;
    private readonly IServicioAutenticacion _servicioAutenticacion;
    private readonly IServicioIncidente _servicioIncidente;
    private readonly int _idChofer;
    private readonly Rutas _rutas;
    public DetallesRutaPage(Rutas rutas, int idChofer,IServicioRutas servicioRutas, IServicioAutenticacion servicioAutenticacion, IServicioIncidente servicioIncidente)
	{
		InitializeComponent();
        _servicioRutas = servicioRutas;
        _servicioAutenticacion = servicioAutenticacion;
        _servicioIncidente = servicioIncidente;
        _idChofer = idChofer;
        _rutas = rutas;
        Carga_Datos(rutas);

    }

    void Carga_Datos(Rutas rutas)
    {
        idRuta = rutas.IdentificadorRuta;
        nombreRuta.Text = rutas.NombreRuta;
        nombreDireccionRuta.Text = rutas.NombreDireccionRuta;
        paisFinal.Text = rutas.PaisFinal;
        ciudadFinal.Text = rutas.CiudadFinal;
        numeroPlaca.Text = rutas.NumeroPlaca;
        idCliente.Text = rutas.IdCliente.ToString();
        nombreCliente.Text = rutas.NombreCliente;
        telefonoCliente.Text = rutas.TelefonoCliente;
        descripcion.Text = rutas.Descripcion;
        fechaInicio.Text = rutas.FechaInicio;
        fechaFinal.Text = rutas.FechaFinal;
        
    }

    async void ActualizarEstadoRutas()
    {
        Rutas rutas = new Rutas()
        {
            IdentificadorRuta = idRuta,
            EstadoEntrega = (string)estadoEntrega.SelectedItem,
        };

        var responseRutas = await _servicioRutas.ActualizarEstadoRuta(rutas);
        if (responseRutas.errores.errorcode == 0)
        {
            await DisplayAlert("Actualización Exitosa", "El estado de la Ruta ha sido actualizado", "OK");
            await Task.Delay(1000);
            await Navigation.PushAsync(new ListaRutasPage(_idChofer, _servicioRutas, _servicioAutenticacion, _servicioIncidente));
        }
        else
        {
            Mensaje.IsVisible = true;
            Mensaje.Text = "Error al actualizar la Ruta";
        }
    }
    private void Actualizar_Clicked(object sender, EventArgs e)
    {
        ActualizarEstadoRutas();
    }
    private async void Crear_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new IncidentesPage(_rutas, _idChofer, _servicioIncidente, _servicioRutas, _servicioAutenticacion));
    }

    async void Regresar()
    {
        await Navigation.PushAsync(new ListaRutasPage(_idChofer, _servicioRutas, _servicioAutenticacion, _servicioIncidente));
    }
    private void Volver_Clicked(object sender, EventArgs e)
    {
        Regresar();
    }
   
}