using dotnet_pdf_library_for_mobiles.Views;
using Microsoft.Extensions.Logging;
#if ANDROID
using dotnet_pdf_library_for_mobiles.Platforms.Android.Handlers;
#elif IOS
using dotnet_pdf_library_for_mobiles.Platforms.iOS.Handlers;
#elif MACCATALYST
using dotnet_pdf_library_for_mobiles.Platforms.MacCatalyst.Handlers;
#endif

namespace dotnet_pdf_library_for_mobiles;

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
            })
            .ConfigureMauiHandlers(handlers =>
            {
#if IOS || MACCATALYST || ANDROID
                handlers.AddHandler<PdfView, PdfViewHandler>();
#endif
            });;

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
