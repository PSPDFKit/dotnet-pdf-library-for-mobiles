using AndroidX.Fragment.App;
using Java.IO;
using Microsoft.Maui.Handlers;
using PSPDFKit.Configuration.Page;
using PSPDFKit.Configuration;
using PSPDFKit.UI;
using Uri = Android.Net.Uri;
using Microsoft.Maui.Controls.Platform;
using dotnet_pdf_library_for_mobiles.Views;

namespace dotnet_pdf_library_for_mobiles.Platforms.Android.Handlers
{
    public partial class PdfViewHandler : ViewHandler<PdfView, FragmentContainerView>, IPdfViewHandler
    {
        private static IPropertyMapper<PdfView, PdfViewHandler> _propertyMapper =
            new PropertyMapper<PdfView, PdfViewHandler>(ViewHandler.ViewMapper);

        public PdfViewHandler() : base(_propertyMapper)
        {
        }

        protected override FragmentContainerView CreatePlatformView()
        {
            return new FragmentContainerView(Platform.CurrentActivity!);
        }

        public PdfFragment PdfFragment { get; private set; }

        public async Task LoadDocumentFromPath(string filePath)
        {
            var jfile = new Java.IO.File(filePath);
            await LoadDocumentAsync(jfile);
        }

        public async Task LoadDocumentFromAssets(string assetName)
        {
            var jfile = await GetFileFromAssets(assetName);
            await LoadDocumentAsync(jfile);
        }

        private async Task LoadDocumentAsync(Java.IO.File jfile)
        {
            Uri docUri = Uri.FromFile(jfile);

            // Get activity from PlatformView's context
            var context = PlatformView.Context;

            // Try to unwrap ContextWrapper to get the base context (which should be the Activity)
            var activity = context as FragmentActivity;
            if (activity == null && context is global::Android.Content.ContextWrapper contextWrapper)
            {
                activity = contextWrapper.BaseContext as FragmentActivity;
            }

            // Prepare fragment
            var configuration = new PdfConfiguration.Builder()
                .LayoutMode(PageLayoutMode.Single)
                .ScrollMode(PageScrollMode.Continuous)
                .ScrollDirection(PageScrollDirection.Vertical)
                .Build();

            PdfFragment = PdfFragment.NewInstance(docUri, configuration);
            PlatformView.EnsureId();

            // Replace PdfView with actual fragment
            activity.SupportFragmentManager
                .BeginTransaction()
                .Replace(PlatformView.Id, PdfFragment)
                .CommitAllowingStateLoss();
        }

        private static async Task<Java.IO.File> GetFileFromAssets(string assetName)
        {
            var activity = await Platform.WaitForActivityAsync();

            var assets = activity.Assets;
            var bytes = default(byte[]);
            using (var reader = new StreamReader(assets.Open(assetName)))
            {
                using (var memstream = new MemoryStream())
                {
                    reader.BaseStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                }
            }

            var tempDir = Path.GetTempPath();
            var filename = Path.Combine(tempDir, assetName);
            Directory.CreateDirectory(tempDir);

            using (var fileOutputStream = new FileOutputStream(filename))
            {
                fileOutputStream.Write(bytes);
            }

            return new Java.IO.File(filename);
        }
    }

}
