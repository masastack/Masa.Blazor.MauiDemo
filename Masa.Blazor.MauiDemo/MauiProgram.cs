using Masa.Blazor.MauiDemo.Platforms;
using Masa.Blazor.MauiDemo.Rcl.Data;
using Masa.Blazor.MauiDemo.Rcl.Services;
using Microsoft.Extensions.Logging;

namespace Masa.Blazor.MauiDemo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMasaBlazorMauiDemo(masaBlazorServiceLifetime: ServiceLifetime.Singleton);
        builder.Services.AddSingleton(_ = new ProDatabase(FileSystem.AppDataDirectory));
        builder.Services.AddSingleton<IPlatformIntegration, MauiPlatformIntegration>();
        builder.Services.AddSingleton<ProductService>();

        builder.Services.AddLogging(logging =>
        {
            logging.AddFilter("Microsoft.AspNetCore.Components.WebView", LogLevel.Trace);
        });
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}