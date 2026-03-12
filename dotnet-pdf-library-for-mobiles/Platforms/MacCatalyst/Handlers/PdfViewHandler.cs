using dotnet_pdf_library_for_mobiles.Views;
using Foundation;
using Microsoft.Maui.Handlers;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace dotnet_pdf_library_for_mobiles.Platforms.MacCatalyst.Handlers
{

    public partial class PdfViewHandler : ViewHandler<PdfView, UIView>, IPdfViewHandler
    {
        private PSPDFViewController? _pdfViewController;
        private UINavigationController? _navigationController;
        private UIViewController? _parentViewController;

        private static readonly IPropertyMapper<PdfView, PdfViewHandler> _propertyMapper =
            new PropertyMapper<PdfView, PdfViewHandler>(ViewHandler.ViewMapper);

        public PdfViewHandler()
            : base(_propertyMapper, null)
        {
        }

        protected override UIView CreatePlatformView()
        {
            var containerView = new UIView
            {
                BackgroundColor = UIColor.White
            };

            return containerView;
        }

        private static UIViewController? FindViewControllerForView(UIView view)
        {
            var next = view.NextResponder;
            while (next != null)
            {
                if (next is UIViewController vc)
                    return vc;
                next = next.NextResponder;
            }
            return null;
        }

        public async Task LoadDocumentFromPath(string filePath)
        {
            var fileUrl = NSUrl.FromFilename(filePath);
            await LoadDocumentFromPath(fileUrl);
        }

        public async Task LoadDocumentFromAssets(string assetName)
        {
            var fileUrl = NSUrl.FromFilename("Contents/Resources/" + assetName);
            await LoadDocumentFromPath(fileUrl);
        }

        public Task LoadDocumentFromPath(NSUrl fileUrl)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                RemovePdfViewController();

                var document = new PSPDFDocument(fileUrl);

                var configuration = PSPDFConfiguration.FromConfigurationBuilder((builder) =>
                {
                    builder.PageMode = PSPDFPageMode.Automatic;
                    builder.PageTransition = PSPDFPageTransition.ScrollPerSpread;
                    builder.ScrollDirection = PSPDFScrollDirection.Vertical;
                });

                _pdfViewController = new PSPDFViewController(document, configuration);

                if (PlatformView is null || _pdfViewController.View is null)
                    return;

                _parentViewController = FindViewControllerForView(PlatformView);
                if (_parentViewController is null)
                    return;

                _navigationController = new UINavigationController(_pdfViewController);

                var navView = _navigationController.View;
                navView.Frame = PlatformView.Bounds;
                navView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

                _parentViewController.AddChildViewController(_navigationController);
                PlatformView.AddSubview(navView);
                _navigationController.DidMoveToParentViewController(_parentViewController);
            });

            return Task.CompletedTask;
        }

        private void RemovePdfViewController()
        {
            if (_navigationController is not null)
            {
                _navigationController.WillMoveToParentViewController(null);
                _navigationController.View?.RemoveFromSuperview();
                _navigationController.RemoveFromParentViewController();
                _navigationController.Dispose();
                _navigationController = null;
            }

            _pdfViewController?.Dispose();
            _pdfViewController = null;
            _parentViewController = null;
        }

        protected override void DisconnectHandler(UIView platformView)
        {
            RemovePdfViewController();
            base.DisconnectHandler(platformView);
        }
    }

}
