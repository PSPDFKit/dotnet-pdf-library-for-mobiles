#if ANDROID
using dotnet_pdf_library_for_mobiles.Platforms.Android.Handlers;
#elif IOS
using dotnet_pdf_library_for_mobiles.Platforms.iOS.Handlers;
#endif

namespace dotnet_pdf_library_for_mobiles.Examples;

public partial class Playground : ContentPage
{
	public Playground()
	{
		InitializeComponent();
#if ANDROID
        PdfView.Handler = new PdfViewHandler();
#elif IOS
        PdfView.Handler = new PdfViewHandler();
#endif
    }
}