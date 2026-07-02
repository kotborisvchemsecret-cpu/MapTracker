using MapTracker;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiMaps();   // now resolves

        return builder.Build();
    }
}
