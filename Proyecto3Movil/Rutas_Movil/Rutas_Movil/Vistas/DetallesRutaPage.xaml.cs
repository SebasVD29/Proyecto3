using Rutas_Movil.Modelos;
using Rutas_Movil.IServicios;

namespace Rutas_Movil.Vistas;

public partial class DetallesRutaPage : ContentPage
{
    private int idRuta = 0;
    private readonly IServicioRutas _servicioRutas;
    public DetallesRutaPage(Rutas rutas, IServicioRutas servicioRutas)
	{
		InitializeComponent();
        _servicioRutas = servicioRutas;
        carga_datos(rutas);

    }

    void carga_datos(Rutas rutas)
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
        estadoEntrega.Text = rutas.EstadoEntrega;

    }

    async void ActualizarEstadoRutas()
    {
        Rutas rutas = new Rutas()
        {
            IdentificadorRuta = idRuta,
            EstadoEntrega = fechaFinal.Text,
        };

        var responseRutas = await _servicioRutas.ActualizarEstadoRuta(rutas);
        if (responseRutas.errores.errorcode == 0)
        {
            await Navigation.PushAsync(new ListaRutasPage(_servicioRutas));
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

        await Navigation.PushAsync(new IncidentesPage(_servicioRutas));
    }

    async void Regresar()
    {
        await Navigation.PushAsync(new ListaRutasPage(_servicioRutas));
    }
    private void Volver_Clicked(object sender, EventArgs e)
    {
        Regresar();
    }
}