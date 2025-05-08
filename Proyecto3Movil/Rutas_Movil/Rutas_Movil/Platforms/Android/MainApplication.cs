using Android.App;
using Android.Runtime;

namespace Rutas_Movil;

#if DEBUG                                   // connect to local service on the
[Application(UsesCleartextTraffic = true)]  // emulator's host for debugging,
#else                                       // access via http://localhost/portnumber
[Application]                               
#endif
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
