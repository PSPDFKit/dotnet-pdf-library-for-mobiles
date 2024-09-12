using Foundation;
using Microsoft.Maui.Handlers;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace dotnet_pdf_library_for_mobiles.Platforms.iOS.Handlers
{

    public partial class PdfViewHandler : ViewHandler<ContentView, UIView>
    {
        private UIView _uiView;

        public PdfViewHandler()
            : base(PdfViewHandler._propertyMapper, null)
        {
        }

        private static IPropertyMapper<ContentView, PdfViewHandler> _propertyMapper =
            new PropertyMapper<ContentView, PdfViewHandler>(ViewHandler.ViewMapper);

        protected override void ConnectHandler(UIView platformView)
        {
            base.ConnectHandler(platformView);
            Dispatcher.GetForCurrentThread().Dispatch(() => LoadDocument());
        }

        protected override void DisconnectHandler(UIView platformView)
        {
            base.DisconnectHandler(platformView);
        }

        protected override UIView CreatePlatformView()
        {
            _uiView = new UIView();
            return _uiView;
        }

        private void LoadDocument()
        {
            // Update to use your document name.
            var document = new PSPDFDocument(NSUrl.FromFilename("document.pdf"));

            // The configuration object is optional and allows additional customization.
            var configuration = PSPDFConfiguration.FromConfigurationBuilder((builder) =>
            {
                builder.PageMode = PSPDFPageMode.Single;
                builder.PageTransition = PSPDFPageTransition.ScrollContinuous;
                builder.ScrollDirection = PSPDFScrollDirection.Vertical;
            });
            var pdfViewController = new PSPDFViewController(document, configuration);

            // Present the PDF view controller within a `UINavigationController` to show built-in toolbar buttons.
            var navController = new UINavigationController(pdfViewController);

            _uiView.Subviews?.ToList().ForEach(v => v.RemoveFromSuperview());
            _uiView.AddSubview(navController.View);
        }
    }

}
