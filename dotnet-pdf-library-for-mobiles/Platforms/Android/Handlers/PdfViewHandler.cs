using AndroidX.Fragment.App;
using Java.IO;
using Microsoft.Maui.Handlers;
using PSPDFKit.Configuration.Page;
using PSPDFKit.Configuration;
using PSPDFKit.UI;
using Uri = Android.Net.Uri;
using Microsoft.Maui.Controls.Platform;
using Android.Hardware;
using dotnet_pdf_library_for_mobiles.Platforms.Android.Listners;
using static AndroidX.Palette.Graphics.Palette;

namespace dotnet_pdf_library_for_mobiles.Platforms.Android.Handlers
{
    public partial class PdfViewHandler : ViewHandler<ContentView, FragmentContainerView>
    {
        private static IPropertyMapper<ContentView, PdfViewHandler> _propertyMapper =
            new PropertyMapper<ContentView, PdfViewHandler>(ViewHandler.ViewMapper);

        internal PdfViewHandler() : base(_propertyMapper)
        {
        }

        protected override FragmentContainerView CreatePlatformView()
        {
            return new FragmentContainerView(Platform.CurrentActivity!);
        }

        public PdfFragment PdfFragment { get; private set; }

        protected override void ConnectHandler(FragmentContainerView platformView)
        {
            base.ConnectHandler(platformView);
            LoadDocument();

            var mauiActivity = Platform.CurrentActivity!;
        }

        protected override void DisconnectHandler(FragmentContainerView platformView)
        {
            base.DisconnectHandler(platformView);
        }

        private async void LoadDocument()
        {
            // Get file
            var jfile = await GetFileFromAssets("document.pdf");
            var docUri = Uri.FromFile(jfile);

            var activity = await Platform.WaitForActivityAsync() as MauiAppCompatActivity;
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
                .Add(PlatformView.Id, PdfFragment)
                .Commit();
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
