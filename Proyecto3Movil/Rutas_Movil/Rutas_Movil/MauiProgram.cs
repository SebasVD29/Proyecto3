using Rutas_Movil.IServicios;
using Rutas_Movil.Servicios;
using Rutas_Movil.Vistas;

namespace Rutas_Movil;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton<ListaRutasPage>();
        builder.Services.AddSingleton<DetallesRutaPage>();
        builder.Services.AddSingleton<IGeneralAPI, GeneralAPI>();
        builder.Services.AddSingleton<IServicioRutas, ServicioRutas>();
        return builder.Build();
	}
}
