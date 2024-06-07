using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;

#if ANDROID
using AndroidX.Fragment.App;
using Java.IO;
using Microsoft.Maui.Platform;
using PSPDFKit.Configuration;
using PSPDFKit.Configuration.Page;
using PSPDFKit.UI;
#endif

namespace dotnet_pdf_library_for_mobiles
{
#if ANDROID

    public partial class PdfViewHandler() : ViewHandler<PdfView, FragmentContainerView>(_propertyMapper)
    {
        private static IPropertyMapper<PdfView, PdfViewHandler> _propertyMapper =
            new PropertyMapper<PdfView, PdfViewHandler>(ViewHandler.ViewMapper);

        protected override FragmentContainerView CreatePlatformView()
        {
            return new FragmentContainerView(Platform.CurrentActivity!);
        }

        protected override void ConnectHandler(FragmentContainerView platformView)
        {
            base.ConnectHandler(platformView);

            ((PdfView)VirtualView).LoadDocument += VirtualView_LoadDocument;
        }

        protected override void DisconnectHandler(FragmentContainerView platformView)
        {
            base.DisconnectHandler(platformView);

            ((PdfView)VirtualView).LoadDocument -= VirtualView_LoadDocument;
        }

        private async void VirtualView_LoadDocument(object? sender, EventArgs e)
        {
            // Get file
            var jfile = await GetFileFromAssets("document.pdf");
            var docUri = Android.Net.Uri.FromFile(jfile);

            var activity = await Platform.WaitForActivityAsync() as MauiAppCompatActivity;

            // Prepare fragment
            var configuration = new PdfConfiguration.Builder()
                .LayoutMode(PageLayoutMode.Single)
                .ScrollMode(PageScrollMode.Continuous)
                .ScrollDirection(PageScrollDirection.Vertical)
                .Build();

            var fragment = PdfFragment.NewInstance(docUri, configuration);

            // Visual check, if code is executed
            PlatformView.SetBackgroundColor(Android.Graphics.Color.LightGreen);
            PlatformView.EnsureId();

            // Replace PdfView with actual fragment
            activity.SupportFragmentManager
                .BeginTransaction()
                .Replace(PlatformView.Id, fragment) // ==> PlatformView.Id is never set / always -1. In Xamarin.Android, you define an id in your xml layout (e.g. android:id="@+id/fragmentContainer") and access
                .Commit();                          //     it in code via Resource.Id.fragmentContainer. In MAUI, I don't have a xml layout with 'android:id="@+id/fragmentContainer"'. I only have my PdfPage.cs.
                                                    //     So if MAUI doesn't provide this id, how do I replace my PdfView control used in the PdfPage with the actual PdfFragment from the PspdfKit Android lib?
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

#elif IOS

    public partial class PdfViewHandler : ContentViewHandler
    {
    }

#endif
}
