using Android.App;
using Android.Content.PM;
using Android.OS;
using PSPDFKit;  // File creation.

namespace dotnet_pdf_library_for_mobiles;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        PSPDFKitGlobal.Initialize(this, licenseKey: null);
    }
}
