global using Plugin.Maui.Audio;
using Android.Media;

namespace Kalk2;

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
                fonts.AddFont("kalk.ttf", "Kalk");
            });
        builder.Services.AddSingleton(AudioManager.Current);
        return builder.Build();
	}
}
