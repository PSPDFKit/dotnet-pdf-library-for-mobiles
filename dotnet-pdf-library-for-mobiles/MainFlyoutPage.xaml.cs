namespace dotnet_pdf_library_for_mobiles;

public partial class MainFlyoutPage : FlyoutPage
{
    public MainFlyoutPage()
    {
        InitializeComponent();
        Loaded += (_, _) => SdkVersionLabel.Text = GetSdkVersion();
    }

    private static string GetSdkVersion()
    {
        #if IOS || MACCATALYST
                return PSPDFKit.Model.PSPDFKitGlobal.VersionString ?? "—";
        #elif ANDROID
                return PSPDFKit.PSPDFKitGlobal.Version ?? "—";
        #else
                return "—";
        #endif
    }

    private void OnPlaygroundClicked(object? sender, EventArgs e)
    {
        Detail = new NavigationPage(new Examples.Playground());
        IsPresented = false;
    }

    private void OnFilePickerClicked(object? sender, EventArgs e)
    {
        Detail = new NavigationPage(new Examples.FilePickerExample());
        IsPresented = false;
    }

    private void OnAboutClicked(object? sender, EventArgs e)
    {
        Detail = new NavigationPage(new About());
        IsPresented = false;
    }
}
