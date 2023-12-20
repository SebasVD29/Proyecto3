namespace Rutas_Movil.Vistas;
using Rutas_Movil.IServicios;

public partial class IncidentesPage : ContentPage
{

        private readonly IServicioIncidente _servicioIncidentes;

        public IncidentesPage(IServicioIncidente servicioIncidentes)
        {
            InitializeComponent();
            _servicioIncidentes = servicioIncidentes;
        }

        private async void CrearIncidencia(object sender, EventArgs e)
        {
            Incidentes.IsVisible = true;

            var nuevoIncidente = new Modelos.Incidente
            {
                Descripcion = txtDescripcion.Text,
                FechaHora = datePickerFecha.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                Solucion = txtSolucion.Text
            };

            var resultado = await _servicioIncidentes.CrearIncidencia(nuevoIncidente);

            Incidentes.IsVisible = false;

        if (resultado != null)
        {
            await DisplayAlert("Éxito", "Incidente creado con éxito", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Error al crear el incidente", "OK");

        }
        }
    }

