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
                PdfView.Handler = new PdfViewHandler();
        }
}