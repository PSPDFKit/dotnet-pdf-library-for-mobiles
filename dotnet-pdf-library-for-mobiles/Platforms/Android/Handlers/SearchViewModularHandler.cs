using Microsoft.Maui.Handlers;
using PSPDFKit.UI.Search;
using Microsoft.Maui.Controls.Platform;

namespace dotnet_pdf_library_for_mobiles.Platforms.Android.Handlers
{
    public partial class SearchViewModularHandler() : ViewHandler<ContentView, PdfSearchViewModular>(_propertyMapper)
    {
        private static IPropertyMapper<ContentView, SearchViewModularHandler> _propertyMapper =
            new PropertyMapper<ContentView, SearchViewModularHandler>(ViewHandler.ViewMapper);

        public PdfSearchViewModular SearchViewModular { get; private set; }

        protected override PdfSearchViewModular CreatePlatformView()
        {
            return new PdfSearchViewModular(Platform.CurrentActivity!);
        }

        protected override void ConnectHandler(PdfSearchViewModular platformView)
        {
            base.ConnectHandler(platformView);
            platformView.EnsureId();
            SearchViewModular = platformView;
        }

        protected override void DisconnectHandler(PdfSearchViewModular platformView)
        {
            base.DisconnectHandler(platformView);
        }
    }

}
