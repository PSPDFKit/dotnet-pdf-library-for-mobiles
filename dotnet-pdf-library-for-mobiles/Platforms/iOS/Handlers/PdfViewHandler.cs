using Foundation;
using Microsoft.Maui.Handlers;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace dotnet_pdf_library_for_mobiles.Platforms.iOS.Handlers
{

    public partial class PdfViewHandler : ViewHandler<ContentView, UIView>
    {
        public PdfViewHandler()
            : base(PdfViewHandler._propertyMapper, null)
        {
        }

        private static IPropertyMapper<ContentView, PdfViewHandler> _propertyMapper =
            new PropertyMapper<ContentView, PdfViewHandler>(ViewHandler.ViewMapper);

        protected override UIView CreatePlatformView()
        {
            var document = new PSPDFDocument(NSUrl.FromFilename("document.pdf"));

            var currentViewController = Platform.GetCurrentUIViewController();

            // The configuration object is optional and allows additional customization.
            var configuration = PSPDFConfiguration.FromConfigurationBuilder((builder) =>
            {
                builder.PageMode = PSPDFPageMode.Automatic;
                builder.PageTransition = PSPDFPageTransition.ScrollPerSpread;
                builder.ScrollDirection = PSPDFScrollDirection.Vertical;
            });
            var pdfViewController = new PSPDFViewController(document, configuration);
            pdfViewController.AnnotationToolbarController.AnnotationToolbar.ToolbarDelegate = new CustomAnnotationToolbarDelegate();

            // Present the PDF view controller within a `UINavigationController` to show built-in toolbar buttons.
            var navController = new UINavigationController(pdfViewController);
            currentViewController.AddChildViewController(navController);
            navController.DidMoveToParentViewController(currentViewController);

            return navController.View;
        }
    }

}
