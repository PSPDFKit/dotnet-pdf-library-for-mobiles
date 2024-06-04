using Android.App;
using Android.Content.PM;
using Android.Content.Res;  // For Assets access.
using Android.OS;
using Java.IO;
using PSPDFKit;  // File creation.
using PSPDFKit.UI;  // To display the PDF.
using PSPDFKit.Configuration.Activity;  // For `PdfActivityConfiguration`.
using PSPDFKit.Configuration.Page;  // For activity configuration properties.

namespace dotnet_pdf_library_for_mobiles;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
  protected override void OnCreate(Bundle? savedInstanceState)
  {
    base.OnCreate(savedInstanceState);

    // Set your `licenseKey` here and initialize PSPDFKit.
    PSPDFKitGlobal.Initialize(this, licenseKey: null);

    // Set your view from the "main" layout resource.
    ShowPdfDocument();
  }

  // Read the contents of your asset.
  Java.IO.File GetFileFromAssets(string assetName)
  {
    AssetManager assets = Assets;
    var bytes = default(byte[]);
    using (StreamReader reader = new StreamReader(assets.Open(assetName)))
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

  // Display the PDF you added to your assets.
  void ShowPdfDocument()
  {
    var jfile = GetFileFromAssets("document.pdf");
    var docUri = Android.Net.Uri.FromFile(jfile);

    // The configuration object is optional and allows additional customization.
    var configuration = new PdfActivityConfiguration.Builder(this)
      .LayoutMode(PageLayoutMode.Single)
      .ScrollMode(PageScrollMode.Continuous)
      .ScrollDirection(PageScrollDirection.Vertical);

    // Show the PDF document.
    PdfActivity.ShowDocument(this, docUri, configuration.Build());
  }
}
