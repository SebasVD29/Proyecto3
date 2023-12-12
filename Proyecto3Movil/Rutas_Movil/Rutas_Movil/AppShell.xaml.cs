using Rutas_Movil.Vistas;

namespace Rutas_Movil;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("rutas", typeof(ListaRutasPage));
        Routing.RegisterRoute("detallerutas", typeof(DetallesRutaPage));
    }
}
